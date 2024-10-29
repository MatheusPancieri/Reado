using Microsoft.EntityFrameworkCore;
using Reado.Api.Data;
using Reado.Domain.Entities;
using Reado.Domain.Handlers;
using Reado.Domain.Request.Books;
using Reado.Domain.Responses;

namespace Reado.Api.Handlers;

public class BookHandler(AppDbContext context) : IBookHandler
{
    public async Task<Response<Book?>> CreateAsync(CreateBookRequest request)
    {
        try
        {
            var book = new Book
            {
                UserId = request.UserId,
                Title = request.Title,
                Author = request.Author,
                Description = request.Description,
                Genre = request.Genre,
                PublicationDate = request.PublicationDate,
                Rating = request.Rating,
            };
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
            return new Response<Book?>(book, 201, "Book created successfully!");
        }
        catch
        {

            return new Response<Book?>(null, 500, "Unable to create book!");
        }
    }

    public async Task<Response<bool>> DeleteAsync(DeleteBookRequest request)
    {
        try
        {
            var book = await context.Books.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            if (book == null)
            {
                return new Response<bool>(false, 404, "Book not Found or not owned by the user");
            }
            await context.SaveChangesAsync();

            return new Response<bool>(true, 200, "Book deleted successfully.");
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, 500, $"Error deleting book: {ex.Message}");
        }
    }

    public async Task<PageResponse<List<Book>>> GetAllAsync(GetAllBooksRequest request)
    {
        try
        {
            var books = await context
            .Books
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId)
            .OrderBy(x => x.Title)
            .ToListAsync();
            var count = books.Count;
            return new PageResponse<List<Book>>(
                books,
                count,
                request.PageNumber,
                request.PageSize
            );
        }
        catch
        {
            return new PageResponse<List<Book>>(null, 500, "Unable to create movie!");
        }
    }

    public async Task<Response<Book?>> GetByIdAsync(GetBookByIdRequest request)
    {
        try
        {
            var book = await context.Books.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            return book is null ? new Response<Book?>(null, 404, "Book Not Found") : new Response<Book?>(book);
        }
        catch (System.Exception)
        {
            return new Response<Book?>(null, 500, "Unable to find Book!");
        }
    }

    public async Task<Response<Book?>> UpdateAsync(UpdateBookRequest request)
    {
        try
        {
            var book = await context.Books.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (book is null)
            {
                return new Response<Book?>(null, 404, "Movie Not Found");
            }
            book.Title = request.Title;
            book.Author = request.Author;
            book.PublicationDate = request.PublicationDate;
            book.Genre = request.Genre;
            book.Description = request.Description;
            await context.SaveChangesAsync();

            return new Response<Book?>(book, 200, "Book Updated");
        }
        catch
        {

            return new Response<Book?>(null, 500, "Unable to find Book!");
        }
    }
}
