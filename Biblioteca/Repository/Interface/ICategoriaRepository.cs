using Biblioteca.Models.Entities;

namespace Biblioteca.Repository.Interface
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetCategoriasAsync();
        Task<Categoria> GetCategoriaByIdAsync(int id);
        Task<Categoria> Adicionar(Categoria categoria);
        Task<CategoriaLivro> Adicionar(CategoriaLivro categoria);
        public void Apagar(Categoria categoria);
        public void Apagar(CategoriaLivro categoria);
        Task<bool> SaveChangesAsync();
        Task<CategoriaLivro> GetCategoriaLivro(int categoriaId, int livroId);
    }
}
