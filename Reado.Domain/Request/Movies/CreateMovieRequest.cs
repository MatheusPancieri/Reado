using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reado.Domain.Enums;

namespace Reado.Domain.Request.Movies;

public class CreateMovieRequest : Request
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "O diretor é obrigatório.")]
    public string Director { get; set; } = string.Empty;

    [Required(ErrorMessage = "Pelo menos um gênero deve ser selecionado.")]
    public Genre Genre { get; set; } = Genre.None;

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data de lançamento é obrigatória.")]
    public DateTime ReleaseDate { get; set; }
}
