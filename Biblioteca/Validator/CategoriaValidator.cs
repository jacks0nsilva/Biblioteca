using Biblioteca.Models.DTO;
using FluentValidation;


namespace Biblioteca.Validator
{
    public class CategoriaValidator : AbstractValidator<CategoriaAdicionarDto>
    {
        public CategoriaValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("O campo nome precisa ser preenchido");
            RuleFor(x => x.Nome).Length(1, 30).WithMessage("O campo nome deve ter no mínimo 1 caracter e no máximo 30 caracteres");
            RuleFor(x => x.Descricao).NotEmpty().WithMessage("O campo descrição precisa ser preenchido");
            RuleFor(x => x.Descricao).Length(1, 150).WithMessage("O campo descrição deve ter no mínimo 1 caracter e no máximo 150 caracteres");
        }
    }
}
