using Biblioteca.Context;
using Biblioteca.Models.Entities;
using Biblioteca.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repository
{
    public class LivroRepository : ILivroRepository
    {
        private readonly BibliotecaContext _context;

        public LivroRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Livro>> GetLivrosAsync()
        {
            var livros = await _context.Livros.Include(x=> x.Autor).Include(x=> x.Categorias).ToListAsync();
            return livros;
        }
        public async Task<Livro> GetLivrosByIdAsync(int id)
        {
            var livro = await _context.Livros
                .Include(x => x.Autor).Include(x => x.Categorias).Where(x=> x.Id == id).FirstOrDefaultAsync();
            return livro;
        }

        public async Task<Livro> Adicionar(Livro livro)
        {
            await _context.Livros.AddAsync(livro);
            return livro;
        }

        public void Atualizar(Livro livro)
        {
            _context.Livros.Update(livro);
        }
        public void Apagar(Livro livro)
        {
            _context.Livros.Remove(livro);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
