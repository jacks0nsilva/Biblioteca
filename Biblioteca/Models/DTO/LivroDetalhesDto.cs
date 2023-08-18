using Biblioteca.Models.Entities;

namespace Biblioteca.Models.DTO
{
    public class LivroDetalhesDto
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string? Sinopse { get; set; }
        public AutorDto? Autor { get; set; }
        public string[]? Categorias { get; set; }
    }
}
