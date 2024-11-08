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
            // Validação dos campos obrigatórios
            if (request.Genres == null || !request.Genres.Any() ||
                request.Authors == null || !request.Authors.Any() ||
                string.IsNullOrWhiteSpace(request.Title))
            {
                return new Response<Recommendation?>(null, 400, "Os campos obrigatórios não podem estar vazios.");
            }

            if (request.MovieList == null || !request.MovieList.Any())
            {
                return new Response<Recommendation?>(null, 400, "A lista de filmes não pode ser vazia.");
            }

            // Buscar as preferências do usuário
            var existingUserPreference = await _context.UserPreferences
                .FirstOrDefaultAsync(up => up.UserId == request.UserId);

            if (existingUserPreference == null)
            {
                return new Response<Recommendation?>(null, 400, "Preferências do usuário não encontradas.");
            }

            // Criar a recomendação, associando o UserPreference existente
            var recommendation = new Recommendation
            {
                UserId = request.UserId,
                UserPreferenceId = existingUserPreference.Id,
                MovieList = request.MovieList,
                Title = request.Title, // Novo campo
                Genres = request.Genres, // Novo campo
                Authors = request.Authors, // Novo campo
                ContentTypes = request.ContentTypes, // Tipo de conteúdo
                Explanation = request.Explanation // Justificativa
            };

            // Adicionar a recomendação ao banco de dados
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
            var userPreference = await _context.UserPreferences
                .FirstOrDefaultAsync(up => up.UserId == request.UserId && up.ProfileName == request.ProfileName)
                ?? throw new Exception("As preferências do usuário não foram encontradas.");

            var movieList = await _context.Movies.Select(m => m.Title).ToListAsync();
            if (movieList == null || !movieList.Any())
                throw new Exception("A lista de filmes disponíveis não pode ser vazia.");

            // Obter as recomendações diretamente do método
            var recommendations = await GetRecommendationsFromOpenAiAsync(userPreference, movieList, request.UserId, userPreference.Id);

            // Salvar as recomendações no banco de dados
            foreach (var recommendation in recommendations)
            {
                _context.Recommendations.Add(recommendation);
            }

            await _context.SaveChangesAsync();

            return new PageResponse<List<Recommendation>>(
                data: recommendations,
                code: 200,
                message: recommendations.Any() ? "Recommendations retrieved successfully." : "No recommendations found."
            );
        }
        // Método para chamar o OpenAI e retornar uma lista de recomendações
        public async Task<List<Recommendation>> GetRecommendationsFromOpenAiAsync(
            UserPreference userPreferences,
            List<string> movieList,
            string userId,
            int userPreferenceId)
        {
            var endpoint = "https://api.openai.com/v1/chat/completions";
            var limitedMovieList = movieList.Take(100).ToList(); // Limitar a lista a 100 filmes

            // Construir a mensagem do sistema
            var systemMessage = $@"
                Você é um assistente especializado em recomendar filmes personalizados. Para priorizar as recomendações, siga estas regras:
                - Gêneros têm prioridade máxima ({string.Join(", ", userPreferences.PreferredGenres)}).
                - Diretores e atores preferidos têm prioridade secundária ({string.Join(", ", userPreferences.PreferredDirectors)}, {string.Join(", ", userPreferences.PreferredActors)}).
                - Temas e filmes favoritos servem como referência ({string.Join(", ", userPreferences.PreferredThemes)}, {string.Join(", ", userPreferences.PreferredMovies)}).
                A lista de filmes disponíveis é: {string.Join(", ", limitedMovieList)}.
                Por favor, recomende **5 filmes** da lista e explique por que eles foram escolhidos.
            ";

            // Mensagem do usuário
            var userMessage = "Com base nas preferências fornecidas, quais filmes você recomenda? Forneça uma lista de 5 filmes com explicações curtas.";

            // Criar o payload para a API OpenAI
            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
            new { role = "system", content = systemMessage },
            new { role = "user", content = userMessage }
        },
                max_tokens = 250, // Configuração para limitar o tamanho da resposta
                temperature = 0.7
            };

            var jsonRequestBody = JsonConvert.SerializeObject(requestBody);

            // Criar a requisição HTTP
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint)
            {
                Headers =
        {
            Authorization = new AuthenticationHeaderValue("Bearer", _openAiApiKey),
            UserAgent = { new ProductInfoHeaderValue("SeuAppNome", "1.0") }
        },
                Content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json")
            };

            // Enviar a requisição
            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Falha na requisição para OpenAI: {response.ReasonPhrase}. Detalhes: {errorContent}");
            }

            // Processar a resposta
            var responseString = await response.Content.ReadAsStringAsync();
            JObject responseObject = JObject.Parse(responseString);

            if (responseObject["choices"] == null || !responseObject["choices"].Any())
            {
                throw new Exception("A resposta da OpenAI não contém recomendações.");
            }

            string recommendedText = responseObject["choices"][0]["message"]["content"].ToString();

            Console.WriteLine("Resposta do OpenAI:");
            Console.WriteLine(recommendedText);

            // Transformar o texto em recomendações
            var recommendations = ParseRecommendations(recommendedText);

            // Preencher campos adicionais em cada recomendação
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

            // Dividir a resposta em blocos para cada filme
            var movieBlocks = responseText.Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var block in movieBlocks)
            {
                var lines = block.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(l => l.Trim()).ToList();

                if (lines.Count > 0)
                {
                    var recommendation = new Recommendation
                    {
                        Title = lines.FirstOrDefault()?.Trim('1', '.', '2', '3', ':', '-').Trim() ?? "Título não disponível",
                        Genres = ExtractField(lines, "Gêneros prioritários:"),
                        Authors = ExtractField(lines, "Diretor preferido:"), // Adaptado para diretores (caso necessário)
                        Explanation = ExtractFieldAsString(lines, "Explicação:") ?? "Nenhuma explicação fornecida."
                    };

                    recommendations.Add(recommendation);
                }
            }

            return recommendations;
        }

        // Método auxiliar para extrair listas de campos (como gêneros ou diretores)
        private static List<string> ExtractField(List<string> lines, string fieldPrefix)
        {
            var fieldLine = lines.FirstOrDefault(line => line.StartsWith(fieldPrefix));
            if (!string.IsNullOrEmpty(fieldLine))
            {
                return fieldLine.Replace(fieldPrefix, "").Trim().Split(',').Select(item => item.Trim()).ToList();
            }

            return new List<string>();
        }

        // Método auxiliar para extrair uma string de campo único (como explicações)
        private static string? ExtractFieldAsString(List<string> lines, string fieldPrefix)
        {
            return lines.FirstOrDefault(line => line.StartsWith(fieldPrefix))?.Replace(fieldPrefix, "").Trim();
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
            // Buscar a recomendação pelo ID
            var recommendation = await _context.Recommendations
                .FirstOrDefaultAsync(r => r.Id == request.Id);

            if (recommendation == null)
            {
                return new Response<Recommendation?>(null, 404, "Recommendation not found.");
            }

            // Atualizar campos apenas se fornecidos no request
            if (request.Genres != null && request.Genres.Any())
                recommendation.Genres = request.Genres;

            if (request.Authors != null && request.Authors.Any())
                recommendation.Authors = request.Authors;

            if (!string.IsNullOrEmpty(request.Title))
                recommendation.Title = request.Title;

            if (!string.IsNullOrEmpty(request.NotificationFrequency))
                recommendation.NotificationFrequency = request.NotificationFrequency;

            if (!string.IsNullOrEmpty(request.ContentTypes))
                recommendation.ContentTypes = request.ContentTypes;

            if (!string.IsNullOrEmpty(request.Explanation))
                recommendation.Explanation = request.Explanation;

            // Atualizar o registro no banco de dados
            _context.Recommendations.Update(recommendation);
            await _context.SaveChangesAsync();

            return new Response<Recommendation?>(recommendation, 200, "Recommendation updated successfully.");
        }

    }
}
