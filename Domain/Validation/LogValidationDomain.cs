using Domain.Entitys.Login;
using Domain.Enums;
using FluentValidation;
using System.Data;

namespace Domain.Validation
{
    public class LogValidationDomain : AbstractValidator<SingOn>
    {
        public LogValidationDomain()
        {
            RuleFor(l => l.Password)
                .NotEmpty()
                    .WithMessage("Senha é obrigatoria")

                .Length(8)
                    .WithMessage("Senha deve conter pelo menos 8 caracteres")

                .Must(l => (l.Any(c => char.IsDigit(c))))
                    .WithMessage("Senha deve conter pelo menos um numero")

                .Must(l => (l.Any(c => char.IsUpper(c))))
                    .WithMessage("Senha deve conter pelo menos um numero maiúscula")

                .Must(l => (l.Any(c => !Char.IsLetterOrDigit(c))))
                    .WithMessage("Senha deve conter pelo menos um carácter especial")

                .Must(l => (l.Any(c => char.IsLetter(c))))
                    .WithMessage("Senha deve conter pelo menos uma letra");


            RuleFor(l => l.Role)
                .Must(l => (Enum.GetNames(typeof(Roles))).Contains(l))
                    .WithMessage("Concessão Invalida");
        }
    }
}
