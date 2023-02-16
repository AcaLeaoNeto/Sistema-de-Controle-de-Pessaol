using Domain.Entitys.Usuario;
using Domain.Interfaces;
using Domain.Validation.BaseValidation;

namespace Domain.Validation.UserValidation
{
    public class UserChangeValidator : UserBaseValidation<UserChange>, IValidations
    {
    }
}
