using Reado.Api.Common.Api;
using Reado.Api.Endpoints.Books;
using Reado.Api.Endpoints.Movies;
using Reado.Domain.Request.Movies;

namespace Reado.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "OK" });

        // Endpoints para Books
        endpoints.MapGroup("v1/books")
            .WithTags("Books")
          //.RequireAuthorization()
            .MapEndpoint<CreateBookEndpoint>()
            .MapEndpoint<UpdateBookEndpoint>()
            .MapEndpoint<DeleteBookEndpoint>()
            .MapEndpoint<GetBookByIdEndpoint>()
            .MapEndpoint<GetAllBooksEndpoint>();

        // Endpoints para Movies
        endpoints.MapGroup("v1/movies")
            .WithTags("Movies")
          //.RequireAuthorization() 
            .MapEndpoint<CreateMovieEndpoint>()
            .MapEndpoint<UpdateMovieEndpoint>()
            .MapEndpoint<DeleteMovieEndpoint>()
            .MapEndpoint<GetMovieByIdEndpoint>()
            .MapEndpoint<GetAllMoviesEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
