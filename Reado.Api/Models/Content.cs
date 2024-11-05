using Reado.Domain.Enums;

namespace Reado.Api.Models
{
    public class Content
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Genre Genre { get; set; } 
        public string Author { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public ContentTypes ContentType { get; set; }
    }
}
