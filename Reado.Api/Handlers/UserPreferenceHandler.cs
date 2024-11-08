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
                // Verifica se já existe uma preferência com o mesmo ProfileName para o mesmo UserId
                var existingPreference = await _context.UserPreferences
                    .FirstOrDefaultAsync(up => up.UserId == request.UserId && up.ProfileName == request.ProfileName);

                if (existingPreference != null)
                {
                    return new Response<UserPreference?>(null, 400, "A user preference with this profile name already exists.");
                }

                // Cria a nova preferência
                var userPreference = new UserPreference
                {
                    UserId = request.UserId,
                    ProfileName = request.ProfileName,
                    PreferredGenres = request.PreferredGenres ?? new List<string>(),
                    PreferredAuthors = request.PreferredAuthors ?? new List<string>(),
                    PreferredDirectors = request.PreferredDirectors ?? new List<string>(),
                    ContentType = request.ContentType,
                    PreferredActors = request.PreferredActors ?? new List<string>(),
                    PreferredMovies = request.PreferredMovies ?? new List<string>(),
                    PreferredThemes = request.PreferredThemes ?? new List<string>(),
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

        public async Task<Response<UserPreference?>> GetProfileNameAsync(GetUserPreferenceByProfileName request)
        {
            try
            {
                var userPreference = await _context.UserPreferences
                    .FirstOrDefaultAsync(up => up.ProfileName == request.ProfileName && up.UserId == request.UserId);

                if (userPreference == null)
                {
                    return new Response<UserPreference?>(null, 404, "User preference with the specified profile name not found.");
                }

                return new Response<UserPreference?>(userPreference, 200, "User preference retrieved successfully.");
            }
            catch (Exception ex)
            {
                return new Response<UserPreference?>(null, 500, $"Unable to retrieve user preference: {ex.Message}");
            }
        }
    }
}
