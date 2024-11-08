using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reado.Domain.Request.Recommendations;

public class GetRecommendationByUserIdRequest : PageRequest
{
    public List<string>? MovieList { get; set; }
    public string ProfileName { get; set; } = string.Empty;

    public string UserPreference { get; set; } = string.Empty;
}
