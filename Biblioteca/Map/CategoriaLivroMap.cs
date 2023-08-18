using Biblioteca.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Map
{
    public class CategoriaLivroMap : IEntityTypeConfiguration<CategoriaLivro>
    {
        public void Configure(EntityTypeBuilder<CategoriaLivro> builder)
        {
        }
    }
}
