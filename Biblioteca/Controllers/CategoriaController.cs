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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categorias = await _repository.GetCategoriasAsync();
            var categoriasRetorno = _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
            return categoriasRetorno.Any() ? Ok(categoriasRetorno) : NotFound("Categorias não encontradas");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categoria = await _repository.GetCategoriaByIdAsync(id);
            if (categoria == null) return NotFound("Categoria não encontrada");
            var categoriaRetorno = _mapper.Map<CategoriaDetalhesDto>(categoria);
            return Ok(categoriaRetorno);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CategoriaAdicionarDto categoria)
        {
            if (categoria == null) return BadRequest("Dados inválidos");
            var categoriaAdiconar = _mapper.Map<Categoria>(categoria);
            await _repository.Adicionar(categoriaAdiconar);
            var status = await _repository.SaveChangesAsync();
            return status ? Ok("Categoria adicionada") : BadRequest("Erro ao adicionar categoria");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _repository.GetCategoriaByIdAsync(id);
            if (categoria == null) return NotFound("Categoria não encontrada");
            _repository.Apagar(categoria);
            var status = await _repository.SaveChangesAsync();
            return status ? Ok("Categoria deletada") : BadRequest("Erro ao deletar categoria");
        }
    }
}
