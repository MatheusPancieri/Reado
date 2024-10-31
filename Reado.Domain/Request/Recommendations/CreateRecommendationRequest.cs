using System.ComponentModel.DataAnnotations;

namespace Reado.Domain.Request.Recommendations;

public class CreateRecommendationRequest : Request
{

    [Required(ErrorMessage = "Preferred genres are required.")]
    public string PreferredGenres { get; set; } = string.Empty;

    [Required(ErrorMessage = "Preferred authors are required.")]
    public string PreferredAuthors { get; set; } = string.Empty;

    [Required(ErrorMessage = "Content types are required.")]
    public string ContentTypes { get; set; } = string.Empty;
}
