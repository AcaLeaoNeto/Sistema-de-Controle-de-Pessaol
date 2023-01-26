using Domain.Entitys;
using Domain.Entitys.Login;

namespace Domain.Interfaces
{
    public interface ILogin : IBase<Login>
    {
        string RegisterLog(Login LogForm);
        Login GetByUsername(string username);
    }
}
