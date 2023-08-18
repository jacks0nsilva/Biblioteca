using Biblioteca.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Map
{
    public class LivroMap : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AutorId).IsRequired();
            builder.HasOne(x => x.Autor).WithMany(x => x.Livros).HasForeignKey(x=> x.AutorId);
            builder.Property(x => x.Titulo).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Sinopse).IsRequired().HasMaxLength(255);
            builder.Property(x => x.QuantidadeDisponivel).IsRequired();
            builder.Property(x => x.QuantidadeEmprestado).IsRequired();
            builder.Property(x => x.QuantidadeTotal).IsRequired();

            builder.HasMany(x => x.Categorias).WithMany(x => x.Livros)
                .UsingEntity<CategoriaLivro>(
                x=> x.HasOne(x=> x.Categoria).WithMany().HasForeignKey(x=> x.CategoriaId),
                x=> x.HasOne(x=> x.Livro).WithMany().HasForeignKey(x=> x.LivroId),
                x =>
                {
                    x.HasKey(p => new { p.CategoriaId, p.LivroId });
                    x.Property(x => x.CategoriaId).IsRequired();
                    x.Property(x => x.LivroId).IsRequired();
                }
                );
        }
    }
}
