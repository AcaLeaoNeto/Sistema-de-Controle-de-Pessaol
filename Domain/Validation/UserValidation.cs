using Domain.Interfaces;
using Domain.Entitys.Usuario;
using Domain.Enums;
using FluentValidation;

namespace Domain.Validation
{
    public class UserValidation : AbstractValidator<UserDto> , IValidations
    {
        public UserValidation()
        {
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
