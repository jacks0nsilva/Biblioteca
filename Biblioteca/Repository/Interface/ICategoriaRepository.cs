using Biblioteca.Models.Entities;

namespace Biblioteca.Repository.Interface
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetCategoriasAsync();
        Task<Categoria> GetCategoriaByIdAsync(int id);
        Task<Categoria> Adicionar(Categoria categoria);
        public void Apagar(Categoria categoria);
        Task<bool> SaveChangesAsync();
    }
}
