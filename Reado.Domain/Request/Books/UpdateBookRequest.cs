using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Reado.Domain.Enums;

namespace Reado.Domain.Request.Books;

public class UpdateBookRequest
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Título inválido")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Author inválido")]
    public string Author { get; set; } = string.Empty;
    public Genre Genre { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime PublicationDate { get; set; }
    public double? Rating { get; set; }
}
