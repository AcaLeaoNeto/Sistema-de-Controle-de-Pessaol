using Domain.Entitys.Login;
using Domain.Entitys.Usuario;

namespace Domain.Interfaces
{
    public interface ILogin : IBase<Log>
    {
        string RegisterLog(Log LogForm);
        Log GetByUsername(string username);
        User GetUserId(int id);
    }
}
