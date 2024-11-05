using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Reado.Api.Common.Api;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.Recommendations;
using Reado.Domain.Responses;
using System.Collections.Generic;

namespace Reado.Api.Endpoints.Recommendations;

public class GetRecommendationForUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/user/recommendations", HandleAsync)
        .WithName("Recommendation: Get For User")
        .WithDescription("Get personalized recommendations for the authenticated user with pagination")
        .WithSummary("Retrieve paginated personalized recommendations for the authenticated user")
        .WithOrder(6)
        .Produces<PageResponse<List<Recommendation>>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IRecommendationHandler handler,
        int pageNumber = 1,
        int pageSize = 10)
    {
        var userId = user.Identity?.Name ?? string.Empty;

        var request = new GetRecommendationForUser
        {
            UserId = userId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        // Chama o handler para obter as recomendações
        var result = await handler.GetForUserAsync(request);

        return result.Data != null && result.Data.Count != 0
            ? TypedResults.Ok(result)
            : TypedResults.NotFound(result);
    }
}
