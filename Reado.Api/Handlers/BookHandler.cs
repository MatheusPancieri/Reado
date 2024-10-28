using Reado.Domain.Entities;
using Reado.Domain.Interfaces;
using Reado.Domain.Request.Books;
using Reado.Domain.Responses;


namespace Reado.Api.Handlers;

public class BookHandler : IBookHandler
{
    public Task<Response<Book?>> CreateAsync(CreateBookRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<bool>> DeleteAsync(DeleteBookRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<List<Book>>> GetAllAsync(GetAllBooksRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Book?>> GetByIdAsync(GetBookByIdRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<Response<Book?>> UpdateAsync(CreateBookRequest request)
    {
        throw new NotImplementedException();
    }
}
