using System.Security.Claims;
using Reado.Api.Common.Api;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.UserPreferences;
using Reado.Domain.Responses;

namespace Reado.Api.Endpoints.UserPreferences;

public class CreateUserPreferenceEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPost("/", HandleAsync)
        .WithName("UserPreference: Create")
        .WithSummary("Creates a new user preference")
        .WithDescription("Creates a new user preference for the user")
        .Produces<Response<UserPreference?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IUserPreferenceHandler handler,
        CreateUserPreferenceRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
