using System.Security.Claims;
using Reado.Api.Common.Api;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.UserPreferences;
using Reado.Domain.Responses;

namespace Reado.Api.Endpoints.UserPreferences;

public class GetUserPreferenceByProfileNameEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/profile/{profileName}", HandleAsync)
        .WithName("UserPreference: Get By Profile Name")
        .WithDescription("Get UserPreference By Profile Name")
        .WithSummary("Retrieve a user preference by profile name")
        .WithOrder(3)
        .Produces<Response<UserPreference?>>()
        .Produces<Response<object>>(400);

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        string profileName,
        IUserPreferenceHandler handler)
    {
        var userId = user.Identity?.Name ?? string.Empty;

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(profileName))
        {
            return TypedResults.BadRequest(new Response<object>(null, 400, "UserId or ProfileName is invalid."));
        }

        // Criação do request
        var request = new GetUserPreferenceByProfileName
        {
            UserId = userId,
            ProfileName = profileName
        };

        // Chamada ao handler
        var result = await handler.GetProfileNameAsync(request);

        // Retorno da resposta
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
