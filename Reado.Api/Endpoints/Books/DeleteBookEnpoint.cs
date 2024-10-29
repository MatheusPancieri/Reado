using System.Security.Claims;
using Reado.Api.Common.Api;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.Books;
using Reado.Domain.Responses;

namespace Reado.Api.Endpoints.Books;

public class DeleteBookEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapDelete("/{id}", HandleAsync)
        .WithName("Book: Delete")
        .WithSummary("Delete Book")
        .WithDescription("Delete Book")
        .WithOrder(3)
        .Produces<Response<Book?>>();
    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IBookHandler handler,
        long id
    )
    {

        var request = new DeleteBookRequest
        {
            UserId = "teste@gmail.com",
            Id = id,
        };
        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
