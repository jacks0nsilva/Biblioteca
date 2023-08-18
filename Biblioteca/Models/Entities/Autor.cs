namespace Biblioteca.Models.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? Nacionalidade { get; set; }
        public List<Livro>? Livros { get; set; }
    }
}
