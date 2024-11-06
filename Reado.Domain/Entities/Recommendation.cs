using System.ComponentModel.DataAnnotations;

namespace Reado.Domain.Entities;

public class Recommendation
{
    [Key]
    public long Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;
    public long UserPreferenceId { get; set; }
    public UserPreference? UserPreference { get; set; }
    public List<string> MovieList { get; set; } = [];
    public string PreferredTitles { get; set; } = string.Empty;

    public string PreferredGenres { get; set; } = string.Empty;

    public string PreferredAuthors { get; set; } = string.Empty;

    public string NotificationFrequency { get; set; } = string.Empty;

    public string ContentTypes { get; set; } = string.Empty;
}
