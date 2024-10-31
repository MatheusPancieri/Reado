using System.Security.Claims;
using Reado.Api.Common.Api;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.Recommendations;
using Reado.Domain.Responses;

namespace Reado.Api.Endpoints.Recommendations;

public class UpdateRecommendationEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPut("/{id}", HandleAsync)
        .WithName("Recommendation: Update")
        .WithDescription("Update Recommendation")
        .WithSummary("Update Recommendation")
        .WithOrder(2)
        .Produces<Response<Recommendation?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IRecommendationHandler handler,
        UpdateRecommendationRequest request,
        long id)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
