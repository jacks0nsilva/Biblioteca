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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var autores = await _repository.GetAutoresAsync();
            var autoresRetorno = _mapper.Map<IEnumerable<AutorDto>>(autores);
            return autoresRetorno.Any() ? Ok(autoresRetorno) : NotFound("Autores não encontrados");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var autor = await _repository.GetAutorByIdAsync(id);
            var autorRetorno = _mapper.Map<AutorDetalhesDto>(autor);
            return autor != null ? Ok(autorRetorno) : NotFound("Autor não encontrado");
        }


        [HttpPost]
        public async Task<IActionResult> Post(AutorAdicionarDto autor)
        {
            if(autor == null) return BadRequest("Dados inválidos");
            var autorAdicionar = _mapper.Map<Autor>(autor);
            await _repository.Adicionar(autorAdicionar);
            var status = await _repository.SaveChangesAsync();
            return status ? Ok("Autor cadastrado") : BadRequest("Autor não cadastrado no banco de dados");
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
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
