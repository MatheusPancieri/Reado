using System.ComponentModel.DataAnnotations;

namespace Reado.Domain.Request;

public class LoginRequest : Request
{
    [Required(ErrorMessage = "E-mail")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha Inválida")]
    public string Password { get; set; } = string.Empty;
}