using Domain.Entitys.Login;
using Domain.Entitys.Usuario;

namespace Domain.Interfaces
{
    public interface ILogin : IBase<Log>
    {
        string RegisterLog(Log LogForm);
        Log GetLogByUsername(string username);
        Guid GetUserId(int id);
        bool AnyLog(string username);
    }
}
