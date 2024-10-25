using Reado.Domain.Entities;
using Reado.Domain.Request.Books;
using Reado.Domain.Responses;

namespace Reado.Domain.Interfaces;

public interface IBookHandler
{
   Task<Response<Book?>> CreateAsync(CreateBookRequest request);

   Task<Response<Book?>> GetByIdAsync(GetBookByIdRequest request);

   Task<Response<List<Book>>> GetAllAsync(GetAllBooksRequest request);

   Task<Response<Book?>> UpdateAsync(CreateBookRequest request);

   Task<Response<bool>> DeleteAsync(DeleteBookRequest request);

}
