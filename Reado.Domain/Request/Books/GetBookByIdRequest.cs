using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reado.Domain.Request.Books;

public class GetBookByIdRequest : Request
{
    public long Id { get; set; }
}
