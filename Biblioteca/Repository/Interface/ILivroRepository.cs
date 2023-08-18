using Biblioteca.Models.Entities;

namespace Biblioteca.Repository.Interface
{
    public interface ILivroRepository
    {
        Task<IEnumerable<Livro>> GetLivrosAsync();
        Task<Livro> GetLivrosByIdAsync(int id);
        Task<Livro> Adicionar(Livro livro);
        public void Atualizar(Livro livro);
        public void Apagar(Livro livro);
        Task<bool> SaveChangesAsync();
    }
}
