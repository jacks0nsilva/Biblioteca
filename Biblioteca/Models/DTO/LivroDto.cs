using Biblioteca.Models.Entities;

namespace Biblioteca.Models.DTO
{
    public class LivroDto
    {
        public string? Titulo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string? Sinopse { get; set; }
        public string[]? Categorias { get; set; }
    }
}
