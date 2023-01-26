using Domain.Entitys;

namespace Services.LoginServices
{
    public interface ILoginServices
    {
        string Login(LoginDto request);
        Login Register(LoginDto request);
    }
}
