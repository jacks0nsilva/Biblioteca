using Biblioteca.Models.DTO;
using FluentValidation;

namespace Biblioteca.Validator
{

    public class AutorValidator : AbstractValidator<AutorAdicionarDto>
    {
        public AutorValidator()
        {
            RuleFor(x=> x.Nome).NotEmpty().WithMessage("O campo nome precisa ser preenchido");
            RuleFor(x => x.Nacionalidade).NotEmpty().WithMessage("O campo nacionalidade precisa ser preenchido");
            RuleFor(x => x.DataNascimento).LessThan(DateTime.Now).WithMessage("Data inválida, verifique os dados");
        }
    }
}
