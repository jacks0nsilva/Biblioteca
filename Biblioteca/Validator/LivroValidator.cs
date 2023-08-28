using Biblioteca.Models.DTO;
using FluentValidation;

namespace Biblioteca.Validator
{
    public class LivroValidator : AbstractValidator<LivroAdicionarDto>
    {
        public LivroValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("O campo título precisa ser preenchido");
            RuleFor(x => x.Titulo).Length(1, 30).WithMessage("O campo sinopse deve ter no mínimo 1 caracter e no máximo 30 caracteres");
            RuleFor(x => x.Sinopse).NotEmpty().WithMessage("O campo sinopse precisa ser preenchido");
            RuleFor(x => x.Sinopse).Length(1, 350).WithMessage("O campo sinopse deve ter no mínimo 1 caracter e no máximo 350 caracteres");
            RuleFor(x => x.DataPublicacao).LessThan(DateTime.Now).WithMessage("Data inválida, verifique os dados");
            RuleFor(x => x.AutorId).GreaterThan(0).WithMessage("AutorId inválido");
        }
    }
}
    