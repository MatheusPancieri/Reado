using Reado.Domain.Enums;

namespace Reado.Domain.Entities
{
    public class UserPreference
    {
        public int Id { get; set; }
        public string ProfileName { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public List<string> PreferredGenres { get; set; } = [];
        public List<string> PreferredAuthors { get; set; } = [];
        public List<string> PreferredDirectors { get; set; } = [];
        public List<string> PreferredMovies { get; set; } = [];
        public List<string> PreferredActors { get; set; } = [];
        public List<string> PreferredThemes { get; set; } = [];
        public ContentTypes ContentType { get; set; } = ContentTypes.Movies;

    }
}
