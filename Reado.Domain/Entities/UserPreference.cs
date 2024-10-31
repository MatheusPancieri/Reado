namespace Reado.Domain.Entities;

public class UserPreference
{
    public string UserId { get; set; } = string.Empty;
    public string PreferredGenres { get; set; } = string.Empty; 
    public string PreferredAuthors { get; set; } = string.Empty; 
    public string PreferredDirectors { get; set; } = string.Empty; 
    public string ContentTypes { get; set; } = "Books,Movies";
}
