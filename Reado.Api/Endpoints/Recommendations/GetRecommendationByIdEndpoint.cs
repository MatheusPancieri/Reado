using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Reado.Api.Common.Api;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.Recommendations;
using Reado.Domain.Responses;

namespace Reado.Api.Endpoints.Recommendations;

public class GetRecommendationByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/{id}", HandleAsync)
        .WithName("Recommendation: Get By Id")
        .WithDescription("Get Recommendation By Id")
        .WithSummary("Get recommendation by id")
        .WithOrder(4)
        .Produces<Response<Recommendation?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IRecommendationHandler handler,
        long id) 
    {
        var request = new GetRecommendationByIdRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
