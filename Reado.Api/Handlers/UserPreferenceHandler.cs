using Microsoft.EntityFrameworkCore;
using Reado.Api.Data;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.UserPreferences;
using Reado.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reado.Api.Handlers
{
    public class UserPreferenceHandler(AppDbContext context) : IUserPreferenceHandler
    {
        private readonly AppDbContext _context = context;

        public async Task<Response<UserPreference?>> CreateAsync(CreateUserPreferenceRequest request)
        {
            try
            {
                var existingPreference = await _context.UserPreferences
                    .FirstOrDefaultAsync(up => up.UserId == request.UserId);

                if (existingPreference != null)
                {
                    return new Response<UserPreference?>(null, 400, "User preference already exists.");
                }

                var userPreference = new UserPreference
                {
                    UserId = request.UserId,
                    PreferredGenres = request.PreferredGenres ?? [],
                    PreferredAuthors = request.PreferredAuthors ?? [],
                    PreferredDirectors = request.PreferredDirectors ?? [],
                    ContentType = request.ContentType
                };

                await _context.UserPreferences.AddAsync(userPreference);
                await _context.SaveChangesAsync();

                return new Response<UserPreference?>(userPreference, 201, "User preference created successfully!");
            }
            catch (Exception ex)
            {
                return new Response<UserPreference?>(null, 500, $"Unable to create user preference: {ex.Message}");
            }
        }
        public async Task<PageResponse<List<UserPreference>>> GetAsync(GetUserPreferenceRequest request)
        {
            try
            {
                var query = _context.UserPreferences.AsQueryable()
                    .Where(up => up.UserId == request.UserId);

                var totalCount = await query.CountAsync();

                var userPreferences = await query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();

                return new PageResponse<List<UserPreference>>(userPreferences, 200, "🦄");
            }
            catch (Exception ex)
            {
                return new PageResponse<List<UserPreference>>(null, 500, $"Unable to find user preferences: {ex.Message}");
            }
        }


    }
}
