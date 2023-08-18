using Biblioteca.Map;
using Biblioteca.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Context
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options):base(options)
        { }

        public DbSet<Autor> Autor { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Livro> Livros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AutorMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new LivroMap());
            base.OnModelCreating(modelBuilder);
        }


    }
}
