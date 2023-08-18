namespace Biblioteca.Models.DTO
{
    public class LivroAdicionarDto
    {
        public string? Titulo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string? Sinopse { get; set; }
        public int AutorId { get; set; }
    }
}
