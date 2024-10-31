using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reado.Domain.Entities;

namespace Reado.Api.Data.Mappings
{
    public class RecommendationMapping : IEntityTypeConfiguration<Recommendation>
    {
        public void Configure(EntityTypeBuilder<Recommendation> builder)
        {
            // Set the table name (optional, Entity Framework uses the class name by default)
            builder.ToTable("Recommendations");

            // Configure the primary key
            builder.HasKey(r => r.Id);

            // Configure columns
            builder.Property(r => r.Id)
                   .ValueGeneratedOnAdd(); // Set as auto-incrementing

            builder.Property(r => r.UserId)
                   .IsRequired()
                   .HasMaxLength(50); // Set a maximum length for string columns if desired

            builder.Property(r => r.PreferredGenres)
                   .HasMaxLength(200); // Optional length constraint

            builder.Property(r => r.PreferredAuthors)
                   .HasMaxLength(200);

            builder.Property(r => r.NotificationFrequency)
                   .HasMaxLength(50);

            builder.Property(r => r.ContentTypes)
                   .HasMaxLength(100);

            // Add any indexes if necessary
            builder.HasIndex(r => r.UserId); // Example index for UserId column
        }
    }
}
