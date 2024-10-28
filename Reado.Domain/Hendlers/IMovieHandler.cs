using Reado.Domain.Entities;
using Reado.Domain.Request.Movies;
using Reado.Domain.Responses;

namespace Reado.Domain.Interfaces;

public interface IMovieHandler
{
    Task<Response<Movie?>> CreateAsync(CreateMovieRequest request);

    Task<Response<Movie?>> GetByIdAsync(GetMovieByIdRequest request);

    Task<PageResponse<List<Movie>>> GetAllAsync(GetAllMoviesRequest request);

    Task<Response<Movie?>> UpdateAsync(UpdateMovieRequest request);

    Task<Response<bool>> DeleteAsync(DeleteMovieRequest request);
}
