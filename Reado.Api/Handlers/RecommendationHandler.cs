using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reado.Api.Data;
using Reado.Domain.Algorithms;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.Recommendations;
using Reado.Domain.Responses;

namespace Reado.Api.Handlers
{
    public class RecommendationHandler(AppDbContext context, RecommendationAlgorithm algorithm) : IRecommendationHandler
    {
        private readonly AppDbContext _context = context;
        private readonly RecommendationAlgorithm _algorithm = algorithm;

        public async Task<Response<Recommendation?>> CreateAsync(CreateRecommendationRequest request)
        {
            var recommendation = new Recommendation
            {
                UserId = request.UserId,
                PreferredGenres = request.PreferredGenres,
                PreferredAuthors = request.PreferredAuthors,
                ContentTypes = request.ContentTypes
            };

            _context.Recommendations.Add(recommendation);
            await _context.SaveChangesAsync();

            return new Response<Recommendation?>(recommendation, 200, message: "Recommendation created successfully.");
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
            var query = _context.Recommendations
                .Where(r => r.UserId == request.UserId);

            var totalItems = await query.CountAsync();
            var recommendations = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PageResponse<List<Recommendation>>(recommendations, totalItems, request.PageNumber, request.PageSize);
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

            _context.Recommendations.Update(recommendation);
            await _context.SaveChangesAsync();

            return new Response<Recommendation?>(recommendation, 200, message: "Recommendation updated successfully.");
        }

        public async Task<PageResponse<List<Recommendation>>> GetForUserAsync(GetRecommendationForUser request)
        {
            // 1. Carrega as preferências do usuário
            var userPreferences = await _context.Recommendations
                .FirstOrDefaultAsync(r => r.UserId == request.UserId);

            if (userPreferences == null)
            {
                return new PageResponse<List<Recommendation>>(null, 0, request.PageNumber, request.PageSize);
            }

            // 2. Carrega todas as recomendações disponíveis
            var allRecommendations = await _context.Recommendations.ToListAsync();

            // 3. Carrega o histórico de interações de outros usuários
            var userHistory = await LoadUserHistoryAsync();

            // 4. Gera as recomendações usando o modelo híbrido
            var personalizedRecommendations = _algorithm.GenerateRecommendations(userPreferences, allRecommendations, userHistory);

            // 5. Pagina os resultados
            var totalItems = personalizedRecommendations.Count;
            var paginatedRecommendations = personalizedRecommendations
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return new PageResponse<List<Recommendation>>(paginatedRecommendations, totalItems, request.PageNumber, request.PageSize);
        }

        private async Task<Dictionary<string, List<Recommendation>>> LoadUserHistoryAsync()
        {
            var history = await _context.Recommendations
                .GroupBy(r => r.UserId)
                .ToDictionaryAsync(g => g.Key, g => g.ToList());

            return history;
        }
    }
}
