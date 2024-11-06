using System.ComponentModel.DataAnnotations;
using Reado.Domain.Entities;
using Reado.Domain.Entities.Account;

namespace Reado.Domain.Request.Recommendations;

public class CreateRecommendationRequest : Request
{
    public int UserPreferenceId { get; set; }
    [Required(ErrorMessage = "UserPreference are required.")]
    public UserPreference? UserPreference { get; set; } 

    [Required(ErrorMessage = "Movie List are required.")]
    public List<string> MovieList { get; set; } = [];

    [Required(ErrorMessage = "Preferred genres are required.")]
    public string PreferredGenres { get; set; } = string.Empty;

    [Required(ErrorMessage = "Preferred titles are required.")]
    public string PreferredTitles { get; set; } = string.Empty;

    [Required(ErrorMessage = "Preferred authors are required.")]
    public string PreferredAuthors { get; set; } = string.Empty;

    [Required(ErrorMessage = "Content types are required.")]
    public string ContentTypes { get; set; } = string.Empty;
}
