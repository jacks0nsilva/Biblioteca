using Biblioteca.Models.Entities;

namespace Biblioteca.Repository.Interface
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> GetAutoresAsync();
        Task<Autor> GetAutorByIdAsync(int id);
        Task<Autor> Adicionar(Autor autor);
        public void Atualizar(Autor autor);
        public void Apagar(Autor autor);

        Task<bool> SaveChangesAsync();
    }
}
