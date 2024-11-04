using System.Security.Claims;
using Reado.Api.Common.Api;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.UserPreferences;
using Reado.Domain.Responses;

namespace Reado.Api.Endpoints.UserPreferences;

public class GetUserPreferenceEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/{id}", HandleAsync)
        .WithName("UserPreference: Get By Id")
        .WithDescription("Get UserPreference By Id")
        .WithSummary("Get UserPreference by id")
        .WithOrder(1)
        .Produces<Response<UserPreference?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IUserPreferenceHandler handler,
        long id)
    {
        var request = new GetUserPreferenceRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.GetAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
