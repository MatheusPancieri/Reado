using Reado.Domain.Enums;

namespace Reado.Domain.Request.UserPreferences
{
    public class CreateUserPreferenceRequest : Request
    {
        public List<string> PreferredGenres { get; set; } = []; 
        public List<string> PreferredAuthors { get; set; } = []; 
        public List<string> PreferredDirectors { get; set; } = []; 
        public ContentTypes ContentType { get; set; } = ContentTypes.Movies; 
    }
}
