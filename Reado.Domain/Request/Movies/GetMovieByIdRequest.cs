using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reado.Domain.Request.Movies;

public class GetMovieByIdRequest : Request
{
    public long Id { get; set; }
}
