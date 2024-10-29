using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reado.Domain.Entities;

namespace Reado.Api.Data.Mappings
{
    public class MovieMapping : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            // Chave primária
            builder.HasKey(m => m.Id);

            // Configurações das colunas
            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(200); // Tamanho máximo de 200 caracteres

            builder.Property(m => m.Director)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(m => m.Genre)
                .IsRequired()
                .HasConversion<string>(); // Salva o Enum como string no banco

            builder.Property(m => m.Rating)
                   .HasPrecision(3, 2);// Exemplo: valor máximo de 10.00

            builder.Property(m => m.Description)
                .HasMaxLength(1000); // Limite de 1000 caracteres

            builder.Property(m => m.ReleaseDate)
                .IsRequired();

            builder.Property(m => m.Recomendations)
                .HasDefaultValue(0); // Valor padrão 0

            builder.Property(m => m.UserId)
                .IsRequired()
                .HasMaxLength(50);

            // Nome da tabela
            builder.ToTable("Movies");
        }
    }
}
