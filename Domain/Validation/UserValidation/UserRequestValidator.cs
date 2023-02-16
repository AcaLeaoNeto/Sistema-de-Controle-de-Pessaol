using Domain.Entitys.Login;
using Domain.Entitys.Usuario;
using Domain.Interfaces;
using Domain.Validation.BaseValidation;
using FluentValidation;

namespace Domain.Validation.UserValidation
{
    public class UserRequestValidator : UserBaseValidation<UserRequest>, IValidations
    {
    }
}
