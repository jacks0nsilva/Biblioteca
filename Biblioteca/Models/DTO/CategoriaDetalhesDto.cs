using Biblioteca.Models.Entities;

namespace Biblioteca.Models.DTO
{
    public class CategoriaDetalhesDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public List<LivroCategoriaDto>? Livros { get; set; }
    }
}
