using AutoMapper;
using Biblioteca.Helper;
using Biblioteca.Models.DTO;
using Biblioteca.Models.Entities;
using Biblioteca.Repository.Interface;
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


        /// <summary>
        /// Obter uma lista de todos os livros
        /// </summary>
        /// <returns>
        /// Lista de livros
        /// </returns>
        /// <response code="200">Sucesso ao retornar lista de livros </response>
        /// <response code="400">Erro ao retornar lista de livro </response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LivroDetalhesDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDefault), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get()
        {
            var livros = await _repository.GetLivrosAsync();
            var livrosRetorno = _mapper.Map <IEnumerable<LivroDetalhesDto>>(livros);
            return livros.Any() 
                ? Ok(livrosRetorno) 
                : BadRequest(new ErrorDefault(StatusCodes.Status400BadRequest, "Não há livros cadastrados"));
        }


        /// <summary>
        /// Obter um livro específico
        /// </summary>
        /// <param name="id">
        /// Identificador do livro
        /// </param>
        /// <returns>Dados do livro</returns>
        /// <response code="200">Sucesso em encontrar o livro </response>
        /// <response code="404">Erro em encontrar o livro </response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LivroDetalhesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDefault), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var livro = await _repository.GetLivrosByIdAsync(id);
            if (livro == null) return NotFound(new ErrorDefault(StatusCodes.Status404NotFound, "Livro não encontrado"));
            var livroRetorno = _mapper.Map<LivroDetalhesDto>(livro);
            return Ok(livroRetorno);
        }


        /// <summary>
        /// Cadastrar um novo livro
        /// </summary>
        /// <param name="livro">
        /// Dados do livro e Id do autor do livro
        /// </param>
        /// <returns>Resultado do cadastro</returns>
        /// <response code="200">Sucesso em cadastrar o livro </response>
        /// <response code="400">Erro de validação ao criar livro </response>
        /// <response code="409">Livro não criado devido a falha no banco de dados</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDefault), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post(LivroAdicionarDto livro)
        {
            var livroAdicionar = _mapper.Map<Livro>(livro);
            await _repository.Adicionar(livroAdicionar);
            var status = await _repository.SaveChangesAsync();
            return status 
                ? Ok("Livro adicionado") 
                : Conflict(new ErrorDefault(StatusCodes.Status409Conflict, "Erro ao adicionar livro"));
        }


        /// <summary>
        /// Atualizar um livro
        /// </summary>
        /// <param name="id">Identificador do livro </param>
        /// <param name="livro">Dados a ser atualizados</param>
        /// <returns>Resultado da requisição</returns>
        /// <response code="404">Livro não encontrado</response>
        /// <response code="200">Sucesso em atualizar o livro </response>
        /// <response code="400">Erro em atualizar o livro </response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ErrorDefault),StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDefault), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, LivroAtualizarDto livro)
        {
            var livroBanco = await _repository.GetLivrosByIdAsync(id);
            if (livroBanco == null) return NotFound(new ErrorDefault(StatusCodes.Status404NotFound, "Livro não encontrado"));
            if(livro.DataPublicacao == new DateTime())
            {
                livro.DataPublicacao = livroBanco.DataPublicacao;
            }

            var livroAtulizar = _mapper.Map(livro, livroBanco);
            _repository.Atualizar(livroAtulizar);
            var status = await _repository.SaveChangesAsync();
            return status 
                ? Ok("Livro atualizado") 
                : BadRequest(new ErrorDefault(StatusCodes.Status400BadRequest, "Erro ao atualizar livro"));
        }


        /// <summary>
        /// Deletar um livro
        /// </summary>
        /// <param name="id">
        /// Identificador do livro
        /// </param>
        /// <returns>Resultado da requisição</returns>
        /// <response code="404">Livro não encontrado</response>
        /// <response code="200">Sucesso em deletar o livro</response>
        /// <response code="400">Erro em deletar o livro</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ErrorDefault), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDefault), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var livro = await _repository.GetLivrosByIdAsync(id);
            if (livro == null) return NotFound(new ErrorDefault(StatusCodes.Status404NotFound, "Livro não encontrado"));
            _repository.Apagar(livro);
            var status = await _repository.SaveChangesAsync();
            return status 
                ? Ok("Livro deletado") 
                : BadRequest(new ErrorDefault(StatusCodes.Status400BadRequest, "Erro ao deletar livro"));
        }

    }
}
