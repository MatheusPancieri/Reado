using Reado.Api.Common.Api;
using Reado.Api.Endpoints.Books;
using Reado.Api.Endpoints.Identity;
using Reado.Api.Endpoints.Identity;
using Reado.Api.Endpoints.Movies;
using Reado.Api.Endpoints.Recommendations;
using Reado.Api.Endpoints.UserPreferences;
using Reado.Api.Models;
using Reado.Domain.Request.UserPreferences;

namespace Reado.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        app.MapGroup("/")
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
        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapEndpoint<LogoutEndpoint>()
            .MapEndpoint<GetRolesEndpoint>();

        // Endpoints para Recommendations
        endpoints.MapGroup("v1/recommendations")
            .WithTags("Recommentations")
            .RequireAuthorization()
            .MapEndpoint<CreateRecommendationEndpoint>()
            .MapEndpoint<GetRecommendationByUserIdEndpoint>()
            .MapEndpoint<UpdateRecommendationEndpoint>()
            .MapEndpoint<GetRecommendationByIdEndpoint>()
            .MapEndpoint<DeleteRecommendationEndpoint>();

        // Endpoints para UserPreferences
        endpoints.MapGroup("v1/userpreferences")
            .WithTags("UserPreferences")
            .RequireAuthorization()
            .MapEndpoint<GetUserPreferenceEndpoint>()
            .MapEndpoint<CreateUserPreferenceEndpoint>()
            .MapEndpoint<GetUserPreferenceByProfileNameEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
