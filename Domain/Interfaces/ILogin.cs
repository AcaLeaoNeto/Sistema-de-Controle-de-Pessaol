using Domain.Entitys.Login;

namespace Domain.Interfaces
{
    public interface ILogin : IBase<Log>
    {
        string RegisterLog(Log LogForm);
        Log GetByUsername(string username);
    }
}
