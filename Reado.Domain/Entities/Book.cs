using Reado.Domain.Enums;

namespace Reado.Domain.Entities;
public class Book
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public Genre Genre { get; set; }
    public float Rating { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime PublicationDate { get; set; }

    public long Recomendations { get; set; }
    public string UserId { get; set; } = string.Empty;
}
