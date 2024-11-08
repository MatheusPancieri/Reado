using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reado.Domain.Entities;
using Reado.Domain.Request.UserPreferences;
using Reado.Domain.Responses;

namespace Reado.Domain.Handlers;

public interface IUserPreferenceHandler
{
    Task<Response<UserPreference?>> CreateAsync(CreateUserPreferenceRequest request);
    Task<PageResponse<List<UserPreference?>>> GetAsync(GetUserPreferenceRequest request);
    Task<Response<UserPreference>> GetProfileNameAsync(GetUserPreferenceByProfileName request);
}
