using System.Security.Claims;
using Reado.Api.Common.Api;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.Books;
using Reado.Domain.Responses;

namespace Reado.Api.Endpoints.Books;

public class CreateBookEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPost("/", HandleAsync)
        .WithName("Book: Create")
        .WithSummary("Create new Book")
        .WithDescription("Create new Book")
        .WithOrder(1)
        .Produces<Response<Book?>>();
    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IBookHandler handler,
        CreateBookRequest request
    )
    {
        request.UserId = "teste@gmail.com";
        var result = await handler.CreateAsync(request);
        return result.IsSuccess
            ? TypedResults.Created($"/{result.Data?.Id}", result)
            : TypedResults.BadRequest(result.Data);
    }
}
