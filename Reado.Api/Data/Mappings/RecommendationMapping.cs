using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reado.Domain.Entities;

namespace Reado.Api.Data.Mappings;

public class RecommendationMapping : IEntityTypeConfiguration<Recommendation>
{
       public void Configure(EntityTypeBuilder<Recommendation> builder)
       {
              builder.HasKey(r => r.Id);

              builder.Property(r => r.UserId)
                  .IsRequired()
                  .HasMaxLength(255);

              builder.Property(r => r.UserPreferenceId)
                  .IsRequired();

              builder.Property(r => r.Title)
                  .IsRequired()
                  .HasMaxLength(1000);

              builder.Property(r => r.Genres)
                  .HasConversion(
                      genres => string.Join(",", genres), // Converter lista para string
                      genres => genres.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList() // Converter string para lista
                  )
                  .HasMaxLength(500);

              builder.Property(r => r.Authors)
                  .HasConversion(
                      authors => string.Join(",", authors), // Converter lista para string
                      authors => authors.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList() // Converter string para lista
                  )
                  .HasMaxLength(500);

              builder.Property(r => r.MovieList)
                  .HasConversion(
                      movies => string.Join(",", movies), // Converter lista para string
                      movies => movies.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList() // Converter string para lista
                  )
                  .HasMaxLength(1000);

              builder.Property(r => r.ContentTypes)
                  .HasMaxLength(255);

              builder.Property(r => r.Explanation)
                  .HasMaxLength(1000);

              builder.ToTable("Recommendations");
       }
}
