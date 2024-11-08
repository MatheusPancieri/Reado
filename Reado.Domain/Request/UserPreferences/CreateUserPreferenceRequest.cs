using Reado.Domain.Entities;
using Reado.Domain.Enums;

namespace Reado.Domain.Request.UserPreferences
{
    public class CreateUserPreferenceRequest : Request
    {
        public string ProfileName { get; set; } = string.Empty;
        public List<string> PreferredGenres { get; set; } = [];
        public List<string> PreferredMovies { get; set; } = [];
        public List<string> PreferredActors { get; set; } = [];
        public List<string> PreferredThemes { get; set; } = [];
        public List<string> PreferredAuthors { get; set; } = [];
        public List<string> PreferredDirectors { get; set; } = [];
        public ContentTypes ContentType { get; set; } = ContentTypes.Movies;
    }
}
