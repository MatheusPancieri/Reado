using Microsoft.EntityFrameworkCore;
using Reado.Api.Data;
using Reado.Domain.Entities;
using Reado.Domain.Interfaces;
using Reado.Domain.Request.Movies;
using Reado.Domain.Responses;

namespace Reado.Api.Handlers;

public class MovieHandler(AppDbContext context) : IMovieHandler
{
    public async Task<Response<Movie?>> CreateAsync(CreateMovieRequest request)
    {
        try
        {
            var movie = new Movie
            {
                UserId = request.UserId,
                Title = request.Title,
                Director = request.Director,
                Genre = request.Genre,
                Description = request.Description,
                ReleaseDate = request.ReleaseDate,
            };
            await context.Movies.AddAsync(movie);
            await context.SaveChangesAsync();
            return new Response<Movie?>(movie, 201, "Movie created successfully!");
        }
        catch
        {

            return new Response<Movie?>(null, 500, "Unable to create movie!");
        }
    }

    public async Task<Response<bool>> DeleteAsync(DeleteMovieRequest request)
    {
        try
        {

            var movie = await context.Movies.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (movie == null)
            {
                return new Response<bool>(false, 404, "Movie not found or not owned by the user.");
            }

            context.Movies.Remove(movie);

            await context.SaveChangesAsync();

            return new Response<bool>(true, 200, "Movie deleted successfully.");
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, 500, $"Error deleting movie: {ex.Message}");
        }
    }

    public async Task<PageResponse<List<Movie>>> GetAllAsync(GetAllMoviesRequest request)
    {
        try
        {
            var movies = await context
                .Movies
                .AsNoTracking()
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Title)
                .ToListAsync();

            var count = movies.Count;

            return new PageResponse<List<Movie>>(
                movies,
                count,
                request.PageNumber,
                request.PageSize
            );
        }
        catch
        {
            // Tratamento da exceção
            return new PageResponse<List<Movie>>(null, 500, "Unable to create movie!");
        }
    }

    public async Task<Response<Movie?>> GetByIdAsync(GetMovieByIdRequest request)
    {
        try
        {
            var movie = await context.Movies.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            return movie is null ? new Response<Movie?>(null, 404, "Movie Not Found") : new Response<Movie?>(movie);
        }
        catch
        {

            return new Response<Movie?>(null, 500, "Unable to find movie!");
        }
    }

    public async Task<Response<Movie?>> UpdateAsync(UpdateMovieRequest request)
    {
        try
        {
            // Buscar o filme pelo Id
            var movie = await context.Movies.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (movie is null)
            {
                return new Response<Movie?>(null, 404, "Movie Not Found");
            }

            movie.Title = request.Title;
            movie.Director = request.Director;
            movie.Genre = request.Genre;
            movie.Description = request.Description;
            movie.ReleaseDate = request.ReleaseDate;

            // Salvar alterações no banco
            await context.SaveChangesAsync();

            return new Response<Movie?>(movie, 200, "Movie Updated"); // Sucesso
        }
        catch (Exception ex)
        {
            // Tratar exceção e retornar erro genérico
            return new Response<Movie?>(null, 500, $"Unable to update movie: {ex.Message}");
        }
    }

}
