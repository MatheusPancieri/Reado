using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reado.Domain.Request.Recommendations;

public class UpdateRecommendationRequest : Request
{
    public long Id { get; set; }
    public string PreferredGenres { get; set; } = string.Empty;

    public string PreferredTitles { get; set; } = string.Empty;
  
    public string PreferredAuthors { get; set; } = string.Empty;

    public string NotificationFrequency { get; set; } = string.Empty;

    public string ContentTypes { get; set; } = string.Empty;
}
