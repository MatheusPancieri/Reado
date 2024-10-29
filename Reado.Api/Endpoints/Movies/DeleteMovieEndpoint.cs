using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Reado.Api.Common.Api;
using Reado.Api.Handlers;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.Movies;
using Reado.Domain.Responses;

namespace Reado.Api.Endpoints.Movies;

public class DeleteMovieEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapDelete("/{id}", HandleAsync)
        .WithName("Movie: Delete")
        .WithDescription("Delete Movie")
        .WithSummary("Delete Movie")
        .WithOrder(3)
        .Produces<Response<Movie?>>();
    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IMovieHandler handler,
        long id)
    {
        var request = new DeleteMovieRequest
        {
            UserId = "teste@gmail.com",
            Id = id
        };
        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
