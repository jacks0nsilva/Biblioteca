﻿using Biblioteca.Context;
using Biblioteca.Models.Entities;
using Biblioteca.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly BibliotecaContext _context;

        public CategoriaRepository(BibliotecaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return categorias;
        }

        public async Task<Categoria> GetCategoriaByIdAsync(int id)
        {
            var categoria = await _context.Categorias
                .Include(x=> x.Livros).ThenInclude(x=> x.Autor).Where(x=> x.Id == id).FirstOrDefaultAsync();
            return categoria;
        }

        public async Task<Categoria> Adicionar(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            return categoria;
        }

        public async Task<CategoriaLivro> Adicionar(CategoriaLivro categoria)
        {
            await _context.CategoriaLivros.AddAsync(categoria);
            return categoria;
        }

        public void Apagar(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
        }

        public void Apagar(CategoriaLivro categoria)
        {
            _context.CategoriaLivros.Remove(categoria);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<CategoriaLivro> GetCategoriaLivro(int categoriaId, int livroId)
        {
            var categoriaLivro = await _context.CategoriaLivros
                 .Where(x => x.CategoriaId == categoriaId && x.LivroId == livroId).FirstOrDefaultAsync();
            return categoriaLivro;
        }
    }
}
