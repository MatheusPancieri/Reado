using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Reado.Api.Common.Api;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.Books;
using Reado.Domain.Request.Movies;
using Reado.Domain.Responses;

namespace Reado.Api.Endpoints.Books;

public class GetMovieByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/{id}", HandleAsync)
        .WithName("Movie: Get By Id")
        .WithDescription("Get Movie By Id")
        .WithSummary("get Movie by id")
        .WithOrder(4)
        .Produces<Response<Movie?>>();
    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IMovieHandler handler,
        long id)
    {
        var request = new GetMovieByIdRequest
        {
            UserId = "teste@gmail.com",
            Id = id
        };

        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
