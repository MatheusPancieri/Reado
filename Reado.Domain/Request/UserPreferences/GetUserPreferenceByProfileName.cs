using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reado.Domain.Request.UserPreferences;

public class GetUserPreferenceByProfileName : Request
{
    public string ProfileName { get; set; } = string.Empty;
}
