using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Reado.Api.Common.Api;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.Recommendations;
using Reado.Domain.Responses;
using System.Collections.Generic;

namespace Reado.Api.Endpoints.Recommendations;

public class GetRecommendationByUserIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/user", HandleAsync)
        .WithName("Recommendation: Get By User ID")
        .WithDescription("Get Recommendations By User ID")
        .WithSummary("Retrieve all recommendations for the authenticated user")
        .WithOrder(3)
        .Produces<Response<IEnumerable<Recommendation>>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IRecommendationHandler handler)
    {
        var userId = user.Identity?.Name ?? string.Empty;
        var request = new GetRecommendationByUserIdRequest
        {
            UserId = userId
        };

        var result = await handler.GetByUserIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
