using Domain.Entitys;
using Domain.Entitys.Login;

namespace Domain.Interfaces
{
    public interface ILogin : IBase<Login>
    {
        Task<string> registerLog(LoginDto LogForm);
    }
}
