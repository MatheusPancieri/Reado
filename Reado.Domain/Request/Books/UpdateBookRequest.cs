using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Reado.Domain.Enums;

namespace Reado.Domain.Request.Books;

public class UpdateBookRequest : Request
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Título inválido")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Author inválido")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "Pelo menos um Genero deve ser selecionado")]
    public Genre Genre { get; set; } = Genre.None;

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Description { get; set; } = string.Empty;
    [Required(ErrorMessage = "A data de publicacao e obrigatoria.")]
    public DateTime PublicationDate { get; set; }
    
    [Required(ErrorMessage = "Ta faltando o Rating")]
    public double? Rating { get; set; }
}
