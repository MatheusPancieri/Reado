using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reado.Domain.Request.Recommendations;

public class DeleteRecommendationRequest : Request
{
    public long Id { get; set; }
}
