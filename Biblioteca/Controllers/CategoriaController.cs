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
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _repository;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        /// <summary>
        /// Obter todos as categorias
        /// </summary>
        /// <returns>Coleção de categorias</returns>
        /// <response code="200">Sucesso ao retornar lista de categorias</response>
        /// <response code="404">Categorias não encontradas no banco de dados</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoriaDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var categorias = await _repository.GetCategoriasAsync();
            var categoriasRetorno = _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
            return categoriasRetorno.Any() ? Ok(categoriasRetorno) : NotFound("Categorias não encontradas");
        }


        /// <summary>
        /// Obter uma categoria
        /// </summary>
        /// <param name="id">Identificador da categoria</param>
        /// <returns>Dados da categoria</returns>
        /// <response code="200">Sucesso em encontrar o autor no banco de dados</response>
        /// <response code="404">Autor não encontrado no banco de dados</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoriaDetalhesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var categoria = await _repository.GetCategoriaByIdAsync(id);
            if (categoria == null) return NotFound("Categoria não encontrada");
            var categoriaRetorno = _mapper.Map<CategoriaDetalhesDto>(categoria);
            return Ok(categoriaRetorno);
        }


        /// <summary>
        /// Cadastrar uma nova categoria
        /// </summary>
        /// <param name="categoria">Dados da categoria</param>
        /// <returns>Resultado do cadastro</returns>
        /// <reponse code="201">Categoria criado com sucesso</reponse>
        /// <response code="400">Erro ao criar a categoria</response>
        [HttpPost]
        [ProducesResponseType(typeof(CategoriaAdicionarDto),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(CategoriaAdicionarDto categoria)
        {
            if (categoria == null) return BadRequest("Dados inválidos");
            var categoriaAdiconar = _mapper.Map<Categoria>(categoria);
            await _repository.Adicionar(categoriaAdiconar);
            var status = await _repository.SaveChangesAsync();
            return status ? Created("", categoria) : BadRequest("Erro ao adicionar categoria");
        }



        /// <summary>
        /// Deletar uma categoria
        /// </summary>
        /// <param name="id">Identificador da categoria</param>
        /// <returns>Resultado da requisição</returns>
        /// <reponse code="200">Categoria deletada com sucesso</reponse>
        /// <response code="400">Erro ao deletar categoria</response>
        /// <response code="404">Categoria não encontrada</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _repository.GetCategoriaByIdAsync(id);
            if (categoria == null) return NotFound("Categoria não encontrada");
            _repository.Apagar(categoria);
            var status = await _repository.SaveChangesAsync();
            return status ? Ok("Categoria deletada") : BadRequest("Erro ao deletar categoria");
        }


        /// <summary>
        /// Criar uma relação entre categoria e livro
        /// </summary>
        /// <param name="categoriaId">Identificador da categoria</param>
        /// <param name="livroId">Identificador do livro</param>
        /// <returns>Resultado da requisição</returns>
        /// <response code="200">Relação já cadastrada no banco de dados</response>
        /// <response code="201">Relação criada com sucesso</response>
        /// <response code="400">Erro ao criar a relação</response>
        [HttpPost("{categoriaId}/adicionar-categoria-livro/{livroId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int categoriaId, int livroId)
        {
            if (categoriaId <= 0 || livroId <= 0) return BadRequest("Dados inválidos");
            var categoriaLivro = await _repository.GetCategoriaLivro(categoriaId, livroId);
            if (categoriaLivro != null) return Ok("Categoria já cadastrada");

            var categoriaLivroAdicionar = new CategoriaLivro {CategoriaId = categoriaId, LivroId = livroId };
            await _repository.Adicionar(categoriaLivroAdicionar);
            var status = await _repository.SaveChangesAsync();
            return status ? Created("", categoriaLivroAdicionar) : BadRequest();
        }



        /// <summary>
        /// Deletar uma relação entre categoria e livro
        /// </summary>
        /// <param name="categoriaId">Identificador da categoria</param>
        /// <param name="livroId">Identificador do livro</param>
        /// <returns>Resultado da requisição</returns>
        /// <response code="404">Relação não cadastrada no banco de dados</response>
        /// <response code="200">Relação deletada com sucesso</response>
        /// <response code="400">Erro ao deletar a relação</response>
        [HttpDelete("{categoriaId}/deletar-categoria-livro/{livroId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int categoriaId, int livroId)
        {
            if (categoriaId <= 0 || livroId <= 0) return ValidationProblem("Identificadores inválidos");
            var categoriaLivro = await _repository.GetCategoriaLivro(categoriaId, livroId);
            if (categoriaLivro == null) return NotFound("Categoria não cadastrada");
            _repository.Apagar(categoriaLivro);
            var status = await _repository.SaveChangesAsync();
            return status ? Ok() : BadRequest();
        }
    }
}
