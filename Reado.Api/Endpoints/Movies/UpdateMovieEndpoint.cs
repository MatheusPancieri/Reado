using System.Security.Claims;
using Reado.Api.Common.Api;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.Movies;
using Reado.Domain.Responses;

namespace Reado.Api.Endpoints.Movies;

public class UpdateMovieEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
=> app.MapPut("/{id}", HandleAsync)
  .WithName("Movie: Update")
  .WithDescription("Update Movie")
  .WithSummary("Update Movie")
  .WithOrder(2)
  .Produces<Response<Movie?>>();
  private static async Task<IResult> HandleAsync(
    ClaimsPrincipal user,
    IMovieHandler handler,
    UpdateMovieRequest request,
    long id)
  {
    request.UserId = user.Identity?.Name ?? string.Empty;
    request.Id = id;
    var result = await handler.UpdateAsync(request);
    return result.IsSuccess
        ? TypedResults.Ok(result)
        : TypedResults.BadRequest(result);
  }
}
