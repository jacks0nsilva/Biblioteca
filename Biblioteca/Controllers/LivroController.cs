using AutoMapper;
using Biblioteca.Models.DTO;
using Biblioteca.Models.Entities;
using Biblioteca.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _repository;
        private readonly IMapper _mapper;
        public LivroController(ILivroRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var livros = await _repository.GetLivrosAsync();
            var livrosRetorno = _mapper.Map <IEnumerable<LivroDetalhesDto>>(livros);
            return livros.Any() ? Ok(livrosRetorno) : BadRequest("Não há livros cadastrados");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var livro = await _repository.GetLivrosByIdAsync(id);
            if (livro == null) return NotFound("Livro não encontrado");
            var livroRetorno = _mapper.Map<LivroDetalhesDto>(livro);
            return Ok(livroRetorno);
        }

        [HttpPost]
        public async Task<IActionResult> Post(LivroAdicionarDto livro)
        {
            if (livro == null) return BadRequest("Dados inválidos");
            var livroAdicionar = _mapper.Map<Livro>(livro);
            await _repository.Adicionar(livroAdicionar);
            var status = await _repository.SaveChangesAsync();
            return status ? Ok("Livro adicionado") : BadRequest("Erro ao adicionar livro");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, LivroAtualizarDto livro)
        {
            var livroBanco = await _repository.GetLivrosByIdAsync(id);
            if (livroBanco == null) return NotFound("Livro não encontrado");
            if(livro.DataPublicacao == new DateTime())
            {
                livro.DataPublicacao = livroBanco.DataPublicacao;
            }

            var livroAtulizar = _mapper.Map(livro, livroBanco);
            _repository.Atualizar(livroAtulizar);
            var status = await _repository.SaveChangesAsync();
            return status ? Ok("Livro atualizado") : BadRequest("Erro ao atualizar livro");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var livro = await _repository.GetLivrosByIdAsync(id);
            if (livro == null) return NotFound("Livro não encontrado");
            _repository.Apagar(livro);
            var status = await _repository.SaveChangesAsync();
            return status ? Ok("Livro deletado") : BadRequest("Erro ao deletar livro");
        }

    }
}
