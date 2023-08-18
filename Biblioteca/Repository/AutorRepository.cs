using Biblioteca.Context;
using Biblioteca.Models.Entities;
using Biblioteca.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repository
{
    public class AutorRepository : IAutorRepository
    {
        private readonly BibliotecaContext _context;

        public AutorRepository(BibliotecaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Autor>> GetAutoresAsync()
        {
            var autores = await _context.Autor.ToListAsync();
            return autores;
        }
        public async Task<Autor> GetAutorByIdAsync(int id)
        {
            var autor = await _context.Autor.Include(x=> x.Livros).ThenInclude(x=> x.Categorias)
                .Where(x=> x.Id == id).FirstOrDefaultAsync();
            return autor;
        }


        public async Task<Autor> Adicionar(Autor autor)
        {
            await _context.Autor.AddAsync(autor);
            return autor;
        }

        public void Atualizar(Autor autor)
        {
            _context.Autor.Update(autor);
        }
        public void Apagar(Autor autor)
        {
            _context.Autor.Remove(autor);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
