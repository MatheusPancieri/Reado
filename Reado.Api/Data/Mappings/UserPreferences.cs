using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reado.Domain.Entities;

namespace Reado.Api.Data.Mappings
{
    public class UserPreferenceMapping : IEntityTypeConfiguration<UserPreference>
    {
        public void Configure(EntityTypeBuilder<UserPreference> builder)
        {
            // Define o nome da tabela
            builder.ToTable("UserPreferences");

            // Configura a chave primária
            builder.HasKey(up => up.Id);

            // Configura as colunas
            builder.Property(up => up.Id)
                   .ValueGeneratedOnAdd(); // Define como auto-incremento

            builder.Property(up => up.UserId)
                   .IsRequired()
                   .HasMaxLength(50); // Define um comprimento máximo para a coluna

            // Configura as colunas de listas de strings como texto
            builder.Property(up => up.PreferredGenres)
                   .HasMaxLength(500) // Define um comprimento máximo para a coluna
                   .HasConversion(
                       v => string.Join(',', v), // Como armazenar no banco
                       v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()) // Como recuperar do banco
                   .IsRequired(false); // Defina como opcional ou obrigatório conforme necessário

            builder.Property(up => up.PreferredAuthors)
                   .HasMaxLength(500)
                   .HasConversion(
                       v => string.Join(',', v),
                       v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
                   .IsRequired(false);

            builder.Property(up => up.PreferredDirectors)
                   .HasMaxLength(500)
                   .HasConversion(
                       v => string.Join(',', v),
                       v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
                   .IsRequired(false);

            // Configura o tipo de conteúdo
            builder.Property(up => up.ContentType)
                   .IsRequired(); // Altere para false se for opcional

            // Adiciona índices se necessário
            builder.HasIndex(up => up.UserId); // Exemplo de índice para a coluna UserId
        }
    }
}
