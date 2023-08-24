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
    public class AutorController : ControllerBase
    {
        private readonly IAutorRepository _repository;
        private readonly IMapper _mapper;

        public AutorController(IAutorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        /// <summary>
        /// Obter todos os autores
        /// </summary>
        /// <returns>
        /// Coleção de autores
        /// </returns>
        /// <response code="200">Sucesso ao retornar lista de autores</response>
        /// <response code="404">Autores não encontrados no banco de dados</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AutorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var autores = await _repository.GetAutoresAsync();
            var autoresRetorno = _mapper.Map<IEnumerable<AutorDto>>(autores);
            return autoresRetorno.Any() ? Ok(autoresRetorno) : NotFound("Autores não encontrados");
        }


        /// <summary>
        /// Obter um autor
        /// </summary>
        /// <param name="id">Identificador do autor</param>
        /// <returns>Dados do autor</returns>
        /// <response code="200">Sucesso em encontrar o autor no banco de dados</response>
        /// <response code="404">Autor não encontrado no banco de dados</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AutorDetalhesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var autor = await _repository.GetAutorByIdAsync(id);
            var autorRetorno = _mapper.Map<AutorDetalhesDto>(autor);
            return autor != null ? Ok(autorRetorno) : NotFound("Autor não encontrado");
        }


        /// <summary>
        /// Cadastrar um novo autor
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="autor">
        /// Nome, data de nascimento e nacionalidade do autor
        /// </param>
        /// <returns>Resultado do cadastro</returns>
        /// <response code="200">Autor criado com sucesso</response>
        /// <response code="400">Erro ao criar o autor</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(AutorAdicionarDto autor)
        {
            if(autor == null) return BadRequest("Dados inválidos");
            var autorAdicionar = _mapper.Map<Autor>(autor);
            await _repository.Adicionar(autorAdicionar);
            var status = await _repository.SaveChangesAsync();
            return status ? Ok("Autor cadastrado") : BadRequest("Autor não cadastrado no banco de dados");
        }


        /// <summary>
        /// Atualizar um autor
        /// </summary>
        /// <param name="id">Identificador do autor</param>
        /// <param name="autor">Nome, data de nascimente e nacionalidade do autor</param>
        /// <returns>Resultado da atualização</returns>
        /// <response code="200">Sucesso ao atualizar o autor</response>
        /// <response code="400">Erro ao atualizar o autor</response>
        /// <response code="404">Autor não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(int id, AutorAtualizarDto autor)
        {
            var autorBanco = await _repository.GetAutorByIdAsync(id);
            if(autorBanco == null) return NotFound("Autor não encontrado");
            if(autor.DataNascimento == new DateTime())
            {
                autor.DataNascimento = autorBanco.DataNascimento;
            }
            var autorAtualizado = _mapper.Map(autor, autorBanco);
            _repository.Atualizar(autorAtualizado);
            var status = await _repository.SaveChangesAsync();
            return status ? Ok("Autor atualizado") : BadRequest("Autor não atualizado no banco de dados");
        }


        /// <summary>
        /// Deletar um autor
        /// </summary>
        /// <param name="id">Identificador do autor</param>
        /// <returns>Resultado da requisição</returns>
        /// <response code="200">Autor deletado com sucesso</response>
        /// <response code="400">Erro ao deletar autor</response>
        /// <response code="404">Autor não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var autor = await _repository.GetAutorByIdAsync(id);
            if (autor == null) return NotFound("Autor não encontrado");
            _repository.Apagar(autor);
            var status = await _repository.SaveChangesAsync();
            return status ? Ok("Autor deletado") : BadRequest("Erro ao delatar o autor");
        }

    }
}
