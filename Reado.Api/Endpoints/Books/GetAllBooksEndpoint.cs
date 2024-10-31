using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Reado.Api.Common.Api;
using Reado.Domain;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.Books;
using Reado.Domain.Responses;

namespace Reado.Api.Endpoints.Books;

public class GetAllBooksEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapGet("/", HandleAsync)
        .WithName("Book: Get All")
        .WithDescription("Get all books")
        .WithSummary("Get all books")
        .WithOrder(5)
        .Produces<PageResponse<List<Book>>>();
    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IBookHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
    )
    {
        var request = new GetAllBooksRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
        var result = await handler.GetAllAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }

}
