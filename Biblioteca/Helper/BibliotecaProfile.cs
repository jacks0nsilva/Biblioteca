using AutoMapper;
using Biblioteca.Models.DTO;
using Biblioteca.Models.Entities;

namespace Biblioteca.Helper
{
    public class BibliotecaProfile : Profile
    {
        public BibliotecaProfile()
        {
            CreateMap<AutorAdicionarDto, Autor>();
            CreateMap<Autor, AutorDto>();
            CreateMap<Autor, AutorDetalhesDto>();
            CreateMap<AutorAtualizarDto, Autor>()
                .ForAllMembers(opt=> opt.Condition((src, dest, srcMember)=> srcMember != null));


            CreateMap<Livro, LivroDto>()
                .ForMember(dest=> dest.Categorias, 
                opt=> opt.MapFrom(src=> src.Categorias.Select(x=> x.Nome).ToArray()));
            CreateMap<Livro, LivroCategoriaDto>()
                .ForMember(dest=> dest.Autor, opt=> opt.MapFrom(src=> src.Autor.Nome));



            CreateMap<Categoria, CategoriaDto>();
            CreateMap<Categoria, CategoriaDetalhesDto>();
            CreateMap<CategoriaAdicionarDto, Categoria>();
        }
    }
}
