using Domain.Enums;
using FluentValidation;
using Domain.Entitys.Base;

namespace Domain.Validation.BaseValidation
{
    public class UserBaseValidation<T> : AbstractValidator<T> where T : UserBaseValitador
    {
        public UserBaseValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 80)
                .WithMessage("Nome deve ter entre 1 a 80 caracteres");

            RuleFor(u => u.DataDeNacimento)
                .LessThan(DateTime.Today)
                    .WithMessage("Data deve ser anterior a hoje")

                .LessThanOrEqualTo(DateTime.Now.AddYears(-18))
                    .WithMessage("Usuario deve ser Maior de idade");


            RuleFor(u => u.Sexo)
                .IsEnumName(typeof(Generos), caseSensitive: false)
                    .WithMessage("Genero Incorreto");


            RuleFor(u => u.Setor)
                .IsEnumName(typeof(Setores), caseSensitive: false)
                    .WithMessage("Setor não encontrado");
        }

    }
}
