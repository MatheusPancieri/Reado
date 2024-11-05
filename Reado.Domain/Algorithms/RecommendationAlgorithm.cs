using System.Collections.Generic;
using System.Linq;
using Reado.Domain.Entities;

namespace Reado.Domain.Algorithms
{
    public class RecommendationAlgorithm
    {
        // Método principal para gerar recomendações usando o modelo híbrido
        public List<Recommendation> GenerateRecommendations(
            Recommendation userPreferences,
            List<Recommendation> allRecommendations,
            Dictionary<string, List<Recommendation>> userHistory)
        {
            // 1. Recomendação baseada em conteúdo (usando gêneros e autores preferidos)
            var contentBasedRecommendations = GetContentBasedRecommendations(userPreferences, allRecommendations);

            // 2. Recomendação colaborativa (usando histórico de usuários semelhantes)
            var collaborativeRecommendations = GetCollaborativeRecommendations(userPreferences.UserId, userHistory, allRecommendations);

            // 3. Combinação dos resultados: Damos uma pontuação maior para recomendações que aparecem em ambos os métodos
            var combinedRecommendations = CombineRecommendations(contentBasedRecommendations, collaborativeRecommendations);

            // 4. Ordena as recomendações por pontuação e retorna a lista final
            return combinedRecommendations
                .OrderByDescending(r => r.Score)
                .Select(r => r.Recommendation)  
                .ToList();
        }

        // Método de filtragem baseada em conteúdo
        private List<ScoredRecommendation> GetContentBasedRecommendations(
            Recommendation userPreferences, List<Recommendation> allRecommendations)
        {
            return allRecommendations
                .Select(r => new ScoredRecommendation
                {
                    Recommendation = r,
                    Score = CalculateContentScore(r, userPreferences)
                })
                .Where(r => r.Score > 0) // Filtra apenas recomendações com pontuação maior que 0
                .ToList();
        }

        // Método de filtragem colaborativa
        private List<ScoredRecommendation> GetCollaborativeRecommendations(
            string userId,
            Dictionary<string, List<Recommendation>> userHistory,
            List<Recommendation> allRecommendations)
        {
            // Filtra usuários semelhantes com base em histórico de preferências e interações
            var similarUsers = userHistory
                .Where(u => u.Key != userId && HasSimilarPreferences(userHistory[userId], u.Value))
                .SelectMany(u => u.Value) // Seleciona as recomendações dos usuários semelhantes
                .Distinct()
                .ToList();

            // Atribui pontuações para recomendações baseadas na frequência de usuários semelhantes
            return similarUsers
                .GroupBy(r => r.Id)
                .Select(g => new ScoredRecommendation
                {
                    Recommendation = g.First(),
                    Score = g.Count() // Pontuação baseada na quantidade de usuários que interagiram com o item
                })
                .ToList();
        }

        // Combina as duas listas de recomendações, somando as pontuações dos itens que estão em ambas
        private List<ScoredRecommendation> CombineRecommendations(
            List<ScoredRecommendation> contentBased, List<ScoredRecommendation> collaborative)
        {
            var combined = new List<ScoredRecommendation>();

            foreach (var recommendation in contentBased)
            {
                var collabMatch = collaborative.FirstOrDefault(c => c.Recommendation.Id == recommendation.Recommendation.Id);
                if (collabMatch != null)
                {
                    // Recomendação encontrada em ambos os métodos, aumenta a pontuação
                    recommendation.Score += collabMatch.Score;
                }
                combined.Add(recommendation);
            }

            // Adiciona as recomendações colaborativas que não estavam na recomendação baseada em conteúdo
            combined.AddRange(collaborative.Where(c => !contentBased.Any(cb => cb.Recommendation.Id == c.Recommendation.Id)));

            return combined;
        }

        // Método para calcular a pontuação de uma recomendação baseada em conteúdo
        private int CalculateContentScore(Recommendation recommendation, Recommendation userPreferences)
        {
            int score = 0;

            // Aumenta a pontuação se o gênero for semelhante
            if (userPreferences.PreferredGenres.Split(',').Any(genre => recommendation.PreferredGenres.Contains(genre)))
                score += 10;

            // Aumenta a pontuação se o autor for semelhante
            if (userPreferences.PreferredAuthors.Split(',').Any(author => recommendation.PreferredAuthors.Contains(author)))
                score += 5;

            return score;
        }

        // Método para verificar se as preferências do histórico são semelhantes
        private bool HasSimilarPreferences(List<Recommendation> userHistory, List<Recommendation> otherUserHistory)
        {
            var userGenres = userHistory.SelectMany(r => r.PreferredGenres.Split(',')).Distinct();
            var otherUserGenres = otherUserHistory.SelectMany(r => r.PreferredGenres.Split(',')).Distinct();

            return userGenres.Intersect(otherUserGenres).Any(); // Checa se há interseção nos gêneros
        }
    }

    // Classe auxiliar para armazenar a pontuação junto com a recomendação
    public class ScoredRecommendation
    {
        public Recommendation Recommendation { get; set; }
        public int Score { get; set; }
    }
}
