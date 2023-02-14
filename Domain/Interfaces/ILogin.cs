using Domain.Entitys.Base;
using Domain.Entitys.Login;
using Domain.Entitys.Usuario;

namespace Domain.Interfaces
{
    public interface ILogin : IBase<Log>
    {
        BaseResponse RegisterLog(Log LogForm);
        Log GetLogByUsername(string username);
        bool AnyLog(string username);
    }
}
