using Biblioteca.Models.Entities;

namespace Biblioteca.Models.DTO
{
    public class AutorAtualizarDto
    {
        public string? Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string? Nacionalidade { get; set; }
    }
}
