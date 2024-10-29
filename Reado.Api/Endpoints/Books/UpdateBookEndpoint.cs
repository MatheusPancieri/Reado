using System.Security.Claims;
using Reado.Api.Common.Api;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.Books;
using Reado.Domain.Responses;

namespace Reado.Api.Endpoints.Books;

public class UpdateBookEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPut("/{id}", HandleAsync)
        .WithName("Book: Update")
        .WithDescription("Update book")
        .WithSummary("Update book")
        .WithOrder(2)
        .Produces<Response<Book?>>();
    private static async Task<IResult> HandleAsync(
    ClaimsPrincipal user,
    IBookHandler handler,
    UpdateBookRequest request,
    long id)
    {
        request.UserId = "teste@gmail.com";
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}
