using Reado.Domain.Enums;

namespace Reado.Domain.Entities;

public class Movie
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public Genre Genre { get; set; } 
    public float Rating { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }

    public long Recomendations { get; set; }
    public string UserId { get; set; } = string.Empty;
}
