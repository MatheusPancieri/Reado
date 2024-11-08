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

    public List<string> MovieList { get; set; } = new List<string>();

    public string Title { get; set; } = string.Empty; // Título principal da recomendação

    public List<string> Genres { get; set; } = new List<string>(); // Gêneros preferidos como lista
    public List<string> Authors { get; set; } = new List<string>(); // Autores relacionados

    public string Explanation { get; set; } = string.Empty; // Justificativa da recomendação

    public string NotificationFrequency { get; set; } = string.Empty; // Frequência de notificações
    public string ContentTypes { get; set; } = string.Empty; // Tipos de conteúdo relacionados
}
