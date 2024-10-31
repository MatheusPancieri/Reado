using System.Security.Claims;
using Reado.Api.Common.Api;
using Reado.Domain.Handlers;
using Reado.Domain.Entities;
using Reado.Domain.Responses;
using Reado.Domain.Request.Movies;

namespace Reado.Api.Endpoints.Movies;

public class CreateMovieEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Movies: Create")
            .WithSummary("Cria um novo filme")
            .WithDescription("Cria um novo filme no catálogo do usuário")
            .Produces<Response<Movie?>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IMovieHandler handler,
        CreateMovieRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
