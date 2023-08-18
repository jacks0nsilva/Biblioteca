namespace Biblioteca.Models.Entities
{
    public class CategoriaLivro
    {
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
    }
}
