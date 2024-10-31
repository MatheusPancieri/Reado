using Reado.Api.Common.Api;
using Reado.Api.Endpoints.Books;
using Reado.Api.Endpoints.Movies;
using Reado.Api.Endpoints.Recommendations;
using Reado.Api.Models;

namespace Reado.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        app.MapGroup("/health")
            .WithTags("Health Check")
            .MapGet("", () => new { message = "OK" });


        // Endpoints para Books
        endpoints.MapGroup("v1/books")
            .WithTags("Books")
            .RequireAuthorization()
            .MapEndpoint<CreateBookEndpoint>()
            .MapEndpoint<UpdateBookEndpoint>()
            .MapEndpoint<DeleteBookEndpoint>()
            .MapEndpoint<GetBookByIdEndpoint>()
            .MapEndpoint<GetAllBooksEndpoint>();

        // Endpoints para Movies
        endpoints.MapGroup("v1/movies")
            .WithTags("Movies")
            .RequireAuthorization()
            .MapEndpoint<CreateMovieEndpoint>()
            .MapEndpoint<UpdateMovieEndpoint>()
            .MapEndpoint<DeleteMovieEndpoint>()
            .MapEndpoint<GetMovieByIdEndpoint>()
            .MapEndpoint<GetAllMoviesEndpoint>();

        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapIdentityApi<User>();

        // Endpoints para Recommendations
        endpoints.MapGroup("v1/recommendations")
            .WithTags("Recommentations")
            .RequireAuthorization()
            .MapEndpoint<CreateRecommendationEndpoint>()
            .MapEndpoint<GetRecommendationByUserIdEndpoint>()
            .MapEndpoint<UpdateRecommendationEndpoint>()
            .MapEndpoint<GetRecommendationByIdEndpoint>()
            .MapEndpoint<DeleteRecommendationEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
