using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Reado.Api.Models;
using Reado.Domain.Entities;
using Reado.Domain.Enums;

namespace RecommendationTestProject.Services
{
    public class ContentAggregatorService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        // Método para obter a lista combinada de filmes e livros
        public async Task<List<Content>> GetCombinedContentAsync()
        {
            var movies = await _httpClient.GetFromJsonAsync<List<Movie>>("api/movies");

            var books = await _httpClient.GetFromJsonAsync<List<Book>>("api/books");

            var combinedContent = new List<Content>();
            if (movies != null)
            {
                combinedContent.AddRange(movies.Select(m => new Content
                {
                    Title = m.Title,
                    Genre = Genre.None,
                    Author = m.Director,
                    Director = m.Director,
                    ContentType = ContentTypes.Movies
                }));
            }

            if (books != null)
            {
                combinedContent.AddRange(books.Select(b => new Content
                {
                    Title = b.Title,
                    Author = b.Author,
                    Genre = Genre.None,
                    ContentType = ContentTypes.Books
                }));
            }

            return combinedContent;
        }
    }
}
