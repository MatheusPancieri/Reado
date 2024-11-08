using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reado.Domain.Request.Recommendations;

public class UpdateRecommendationRequest : Request
{
    [Required(ErrorMessage = "Id is required.")]
    public long Id { get; set; } // ID da recomendação a ser atualizada

    [Required(ErrorMessage = "Genres are required.")]
    public List<string> Genres { get; set; } = new List<string>(); // Lista de gêneros

    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; } = string.Empty; // Título da recomendação

    [Required(ErrorMessage = "Authors are required.")]
    public List<string> Authors { get; set; } = new List<string>(); // Lista de autores

    public string NotificationFrequency { get; set; } = string.Empty; // Frequência de notificações
    public string ContentTypes { get; set; } = string.Empty; // Tipos de conteúdo
    public string Explanation { get; set; } = string.Empty; // Justificativa ou descrição opcional
}
