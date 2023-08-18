using Biblioteca.Models.Entities;

namespace Biblioteca.Models.DTO
{
    public class LivroCategoriaDto
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string? Sinopse { get; set; }
        public string? Autor { get; set; }
    }
}
