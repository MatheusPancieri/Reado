using System.Security.Claims;
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
        .WithName("Recommendation: Get By User ID")
        .WithDescription("Get Recommendations By User ID")
        .WithSummary("Retrieve all recommendations for the authenticated user")
        .WithOrder(3)
        .Produces<PageResponse<IEnumerable<Recommendation>>>(); 

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IRecommendationHandler handler)
    {
        var userId = user.Identity?.Name ?? string.Empty;
        var request = new GetRecommendationByUserIdRequest
        {
            UserId = userId
        };

        // Chama o método GetByUserIdAsync do handler para buscar recomendações do banco ou OpenAI
        var result = await handler.GetByUserIdAsync(request);

        // Verifica se a resposta contém recomendações, caso contrário, retorne as do OpenAI
        if (result.IsSuccess && result.Data.Any())
        {
            // Se houver recomendações no banco, retorna as mesmas
            return TypedResults.Ok(result);
        }
        else
        {
            // Se não houver recomendações no banco, já foi chamado o OpenAI dentro do método handler
            var openAiRecommendations = result.Data;
            
            // Retorna as recomendações do OpenAI ou do banco
            return TypedResults.Ok(new PageResponse<List<Recommendation>>(openAiRecommendations, openAiRecommendations.Count, request.PageNumber, request.PageSize));
        }
    }
}
