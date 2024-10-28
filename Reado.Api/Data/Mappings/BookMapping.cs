using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reado.Domain.Entities;

namespace Reado.Api.Data.Mappings
{
    public class BookMapping : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            // Definindo a chave primária
            builder.HasKey(b => b.Id);

            // Configurações das colunas
            builder.Property(b => b.Title)
                .IsRequired() // Campo obrigatório
                .HasMaxLength(200); // Tamanho máximo de 200 caracteres

            builder.Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(b => b.Genre)
                .IsRequired()
                .HasConversion<string>(); // Salva o Enum como string no banco

            builder.Property(b => b.Rating)
                .HasPrecision(3, 2); // Ex.: Valor máximo 10.00

            builder.Property(b => b.Description)
                .HasMaxLength(1000); // Limite de 1000 caracteres

            builder.Property(b => b.PublicationDate)
                .IsRequired();

            builder.Property(b => b.Recomendations)
                .HasDefaultValue(0); // Valor padrão 0

            builder.Property(b => b.UserId)
                .IsRequired()
                .HasMaxLength(50);

            // Nome da tabela
            builder.ToTable("Books");
        }
    }
}
