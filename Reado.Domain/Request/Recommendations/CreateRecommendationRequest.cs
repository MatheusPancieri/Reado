using System.ComponentModel.DataAnnotations;
using Reado.Domain.Entities;

namespace Reado.Domain.Request.Recommendations;

public class CreateRecommendationRequest : Request
{
    public int UserPreferenceId { get; set; }

    [Required(ErrorMessage = "UserPreference is required.")]
    public UserPreference? UserPreference { get; set; }

    [Required(ErrorMessage = "Movie List is required.")]
    public List<string> MovieList { get; set; } = new List<string>();

    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; } = string.Empty; // Título principal da recomendação

    [Required(ErrorMessage = "Genres are required.")]
    public List<string> Genres { get; set; } = new List<string>(); // Lista de gêneros

    [Required(ErrorMessage = "Authors are required.")]
    public List<string> Authors { get; set; } = new List<string>(); // Lista de autores

    [Required(ErrorMessage = "Content types are required.")]
    public string ContentTypes { get; set; } = string.Empty;

    public string Explanation { get; set; } = string.Empty; // Campo opcional para justificativa
}
