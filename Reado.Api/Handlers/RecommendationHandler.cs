using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reado.Api.Data;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.Recommendations;
using Reado.Domain.Responses;

namespace Reado.Api.Handlers
{
    public class RecommendationHandler(AppDbContext context, HttpClient httpClient, IOptions<OpenAIOptions> options) : IRecommendationHandler
    {
        private readonly AppDbContext _context = context;
        private readonly HttpClient _httpClient = httpClient;
        private readonly string _openAiApiKey = options.Value.ApiKey;

        public async Task<Response<Recommendation?>> CreateAsync(CreateRecommendationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.PreferredGenres) ||
                string.IsNullOrWhiteSpace(request.PreferredAuthors) ||
                string.IsNullOrWhiteSpace(request.PreferredTitles))
            {
                return new Response<Recommendation?>(null, 400, "As preferências do usuário não podem ser vazias.");
            }

            if (request.MovieList == null || !request.MovieList.Any())
            {
                return new Response<Recommendation?>(null, 400, "A lista de filmes não pode ser vazia.");
            }

            var existingUserPreference = await _context.UserPreferences
                .FirstOrDefaultAsync(up => up.UserId == request.UserId);

            if (existingUserPreference == null)
            {
                return new Response<Recommendation?>(null, 400, "Preferências do usuário não encontradas.");
            }

            // Cria a recomendação, associando o UserPreference existente
            var recommendation = new Recommendation
            {
                UserId = request.UserId,
                UserPreferenceId = existingUserPreference.Id,
                MovieList = request.MovieList,
                PreferredAuthors = request.PreferredAuthors,
                PreferredGenres = request.PreferredGenres,
                PreferredTitles = request.PreferredTitles,
                ContentTypes = request.ContentTypes
            };

            _context.Recommendations.Add(recommendation);
            await _context.SaveChangesAsync();

            return new Response<Recommendation?>(recommendation, 200, "Recommendation created successfully.");
        }

        public async Task<Response<Recommendation?>> DeleteAsync(DeleteRecommendationRequest request)
        {
            var recommendation = await _context.Recommendations
                .FirstOrDefaultAsync(r => r.Id == request.Id);

            if (recommendation == null)
            {
                return new Response<Recommendation?>(null, 200, message: "Recommendation not found.");
            }

            _context.Recommendations.Remove(recommendation);
            await _context.SaveChangesAsync();

            return new Response<Recommendation?>(null, 200, message: "Recommendation deleted successfully.");
        }

        public async Task<PageResponse<List<Recommendation>>> GetByUserIdAsync(GetRecommendationByUserIdRequest request)
        {
            // Buscar as preferências do usuário no banco de dados
            var userPreference = await _context.UserPreferences
                .FirstOrDefaultAsync(up => up.UserId == request.UserId && up.ProfileName == request.ProfileName) ?? throw new Exception("As preferências do usuário não foram encontradas.");
            // Obter a lista de filmes disponíveis
            var movieList = await _context.Movies.Select(m => m.Title).ToListAsync();

            if (movieList == null || movieList.Count == 0)
                throw new Exception("A lista de filmes disponíveis não pode ser vazia.");

            // Construir o texto das preferências do usuário para enviar ao OpenAI
            

            // Chamar o OpenAI para gerar as recomendações
            var openAiRecommendations = await GetRecommendationsFromOpenAiAsync(userPreference, movieList, request.UserId, userPreference.Id);

            return new PageResponse<List<Recommendation>>(
                data: openAiRecommendations,
                code: 200,
                message: "Recommendations retrieved successfully."
            );
        }

        // Método para chamar o OpenAI e retornar uma lista de recomendações
        public async Task<List<Recommendation>> GetRecommendationsFromOpenAiAsync(UserPreference userPreferences, List<string> movieList, string userId, int userPreferenceId)
        {
            var endpoint = "https://api.openai.com/v1/chat/completions";
            var limitedMovieList = movieList.Take(100).ToList(); // Aumentado para 100 filmes

            var systemMessage = $@"
                Você é um assistente especializado em recomendar filmes personalizados. Para priorizar as recomendações, siga estas regras:
                - Gêneros têm prioridade máxima ({string.Join(", ", userPreferences.PreferredGenres)}).
                - Diretores e atores preferidos têm prioridade secundária ({string.Join(", ", userPreferences.PreferredDirectors)}, {string.Join(", ", userPreferences.PreferredActors)}).
                - Temas e filmes favoritos servem como referência ({string.Join(", ", userPreferences.PreferredThemes)}, {string.Join(", ", userPreferences.PreferredMovies)}).
                Selecione **5 filmes** da lista fornecida e explique sua escolha.
                ";
            var userMessage = $"{userPreferences} Quais filmes você recomenda? Por favor, forneça uma lista de 5 filmes recomendados, apenas títulos, um por linha.";

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
            new { role = "system", content = systemMessage },
            new { role = "user", content = userMessage }
        },
                max_tokens = 250, // Aumentado para acomodar a resposta
                temperature = 0.7
            };

            var jsonRequestBody = JsonConvert.SerializeObject(requestBody);
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _openAiApiKey);
            request.Headers.UserAgent.ParseAdd("SeuAppNome/1.0");
            request.Content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Falha na requisição para OpenAI: {response.ReasonPhrase}. Detalhes: {errorContent}");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            JObject responseObject = JObject.Parse(responseString);

            string recommendedText = responseObject["choices"][0]["message"]["content"].ToString();

            // Exibir a resposta para depuração
            Console.WriteLine("Resposta do OpenAI:");
            Console.WriteLine(recommendedText);

            var recommendations = ParseRecommendations(recommendedText);

            // Preencher campos adicionais
            recommendations = recommendations.Select(r =>
            {
                r.UserId = userId;
                r.UserPreferenceId = userPreferenceId;
                return r;
            }).ToList();

            return recommendations;
        }


        // Método auxiliar para converter a resposta do OpenAI em uma lista de Recommendation
        private static List<Recommendation> ParseRecommendations(string responseText)
        {
            var recommendations = new List<Recommendation>();
            var titlesSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase); // Usar HashSet para evitar duplicatas

            // Dividir a resposta em linhas
            var lines = responseText.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                // Remover possíveis caracteres de numeração e espaços
                var title = line.Trim().TrimStart(new char[] { '-', '*', '•' }).Trim();

                // Remover numeração se houver
                title = Regex.Replace(title, @"^\d+\.\s*", "");

                if (!string.IsNullOrEmpty(title))
                {
                    // Adicionar ao conjunto e à lista se ainda não estiver presente
                    if (titlesSet.Add(title))
                    {
                        recommendations.Add(new Recommendation { PreferredTitles = title });
                    }
                }
            }

            return recommendations;
        }

        public async Task<Response<Recommendation?>> GetByIdAsync(GetRecommendationByIdRequest request)
        {
            var recommendation = await _context.Recommendations
                .FirstOrDefaultAsync(r => r.Id == request.Id);

            if (recommendation == null)
            {
                return new Response<Recommendation?>(null, 200, message: "Recommendation not found.");
            }

            return new Response<Recommendation?>(recommendation, 200);
        }

        public async Task<Response<Recommendation?>> UpdateAsync(UpdateRecommendationRequest request)
        {
            var recommendation = await _context.Recommendations
                .FirstOrDefaultAsync(r => r.UserId == request.UserId);

            if (recommendation == null)
            {
                return new Response<Recommendation?>(null, 200, message: "Recommendation not found.");
            }
            if (!string.IsNullOrEmpty(request.PreferredGenres))
                recommendation.PreferredGenres = request.PreferredGenres;

            if (!string.IsNullOrEmpty(request.PreferredAuthors))
                recommendation.PreferredAuthors = request.PreferredAuthors;

            if (!string.IsNullOrEmpty(request.NotificationFrequency))
                recommendation.NotificationFrequency = request.NotificationFrequency;

            if (!string.IsNullOrEmpty(request.ContentTypes))
                recommendation.ContentTypes = request.ContentTypes;

            if (!string.IsNullOrEmpty(request.PreferredTitles))
                recommendation.PreferredTitles = request.PreferredTitles;

            _context.Recommendations.Update(recommendation);
            await _context.SaveChangesAsync();

            return new Response<Recommendation?>(recommendation, 200, message: "Recommendation updated successfully.");
        }

    }
}
