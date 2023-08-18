namespace Biblioteca.Models.Entities
{
    public class Livro
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string? Sinopse { get; set; }
        public int QuantidadeTotal { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public int QuantidadeEmprestado { get; set; }

        public int AutorId { get; set; }
        public Autor? Autor { get; set; }
        public List<Categoria>? Categorias { get; set; }
    }
}
