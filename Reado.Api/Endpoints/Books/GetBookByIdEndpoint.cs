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
using Reado.Domain.Responses;

namespace Reado.Api.Endpoints.Books;

public class GetBookByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/{id}", HandleAsync)
        .WithName("Book: Get By Id")
        .WithDescription("Get Book By Id")
        .WithSummary("get book by id")
        .WithOrder(4)
        .Produces<Response<Book?>>();
    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IBookHandler handler,
        long id)
    {
        var request = new GetBookByIdRequest
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
