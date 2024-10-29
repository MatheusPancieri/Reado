using System;
using System.ComponentModel.DataAnnotations;
using Reado.Domain.Enums;

namespace Reado.Domain.Request.Books;

public class CreateBookRequest : Request
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "O autor é obrigatório.")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "Pelo menos um gênero deve ser selecionado.")]
    public Genre Genre { get; set; } = Genre.None;

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data de publicação é obrigatória.")]
    public DateTime PublicationDate { get; set; }
    public float Rating { get; set; }  
}
