using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reado.Api.Common.Api;
using Reado.Domain;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.Movies;
using Reado.Domain.Responses;

namespace Reado.Api.Endpoints.Movies;

public class GetAllMoviesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
=> app.MapGet("/", HandleAsync)
    .WithName("Movie: Get All Movies")
    .WithDescription("Get All Movies")
    .WithSummary("Get All Movies")
    .WithOrder(5)
    .Produces<Response<Movie?>>();
    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IMovieHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllMoviesRequest
        {
            UserId = "teste@gmail.com",
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
        var result = await handler.GetAllAsync(request);
        return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
    }
}
