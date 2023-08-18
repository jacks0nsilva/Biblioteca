namespace Biblioteca.Models.DTO
{
    public class AutorDetalhesDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? Nacionalidade { get; set; }
        public List<LivroDto>? Livros { get; set; }
    }
}
