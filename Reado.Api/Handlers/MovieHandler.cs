using Reado.Api.Data;
using Reado.Domain.Entities;
using Reado.Domain.Interfaces;
using Reado.Domain.Request.Movies;
using Reado.Domain.Responses;

namespace Reado.Api.Handlers;

public class MovieHandler(AppDbContext context) : IMovieHandler
{
    public Task<Response<Movie?>> CreateAsync(CreateMovieRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<bool>> DeleteAsync(DeleteMovieRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<List<Movie>>> GetAllAsync(GetAllMoviesRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Movie?>> GetByIdAsync(GetMovieByIdRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Movie?>> UpdateAsync(CreateMovieRequest request)
    {
        throw new NotImplementedException();
    }
}
