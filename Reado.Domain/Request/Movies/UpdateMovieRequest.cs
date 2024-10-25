
using System.ComponentModel.DataAnnotations;
using Reado.Domain.Enums;

namespace Reado.Domain.Request.Movies;

public class UpdateMovieRequest : Request
{
    public long Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "O diretor é obrigatório.")]
    public string Director { get; set; } = string.Empty;

    [Required(ErrorMessage = "Pelo menos um gênero deve ser selecionado.")]
    public Genre Genre { get; set; } 

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data de lançamento é obrigatória.")]
    public DateTime ReleaseDate { get; set; }
}
