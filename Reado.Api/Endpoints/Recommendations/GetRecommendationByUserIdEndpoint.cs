using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Reado.Api.Common.Api;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.Recommendations;
using Reado.Domain.Responses;

namespace Reado.Api.Endpoints.Recommendations;

public class GetRecommendationByUserIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/user", HandleAsync)
        .WithName("Recommendation: Get By User ID and Profile Name")
        .WithDescription("Get Recommendations By User ID and Profile Name")
        .WithSummary("Retrieve recommendations for the authenticated user based on a specific profile")
        .WithOrder(3)
        .Produces<PageResponse<IEnumerable<Recommendation>>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        [FromQuery] string profileName, // Permitir que o usuário forneça o ProfileName via query string
        IRecommendationHandler handler)
    {
        var userId = user.Identity?.Name ?? string.Empty;

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(profileName))
        {
            return TypedResults.BadRequest(new Response<object>(null, 400, "User ID or Profile Name is missing."));
        }

        var request = new GetRecommendationByUserIdRequest
        {
            UserId = userId,
            ProfileName = profileName // Adiciona o ProfileName ao request
        };

        // Chama o método GetByUserIdAsync do handler para buscar recomendações do banco ou OpenAI
        var result = await handler.GetByUserIdAsync(request);

        if (result.IsSuccess && result.Data.Any())
        {
            return TypedResults.Ok(result);
        }
        else
        {
            // Se não houver recomendações no banco, já foi chamado o OpenAI dentro do handler
            return TypedResults.Ok(new PageResponse<List<Recommendation>>(result.Data, result.Data.Count, request.PageNumber, request.PageSize));
        }
    }
}
