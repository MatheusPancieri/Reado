using System.Security.Claims;
using Reado.Api.Common.Api;
using Reado.Domain.Handlers;
using Reado.Domain.Responses;
using Reado.Domain.Request.Recommendations;

namespace Reado.Api.Endpoints.Recommendations;

public class DeleteRecommendationEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/recommendations/{id}", HandleAsync)
            .WithName("Recommendations: Delete")
            .WithSummary("Deletes a recommendation")
            .WithDescription("Deletes a recommendation by ID for the authenticated user")
            .Produces<Response<bool>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IRecommendationHandler handler,
        long id) 
    {
        var userId = user.Identity?.Name ?? string.Empty;
        var request = new DeleteRecommendationRequest
        {
            UserId = userId,
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
