using Domain.Entitys.Login;

namespace Services.LoginServices
{
    public interface ILoginServices
    {
        string Login(LoginDto request);
        string Register(LoginDto request);
    }
}
