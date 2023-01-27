using Domain.Entitys.Login;
using Microsoft.AspNetCore.Mvc;

namespace Services.LoginServices
{
    public interface ILoginServices
    {
        string Login(LoginSingIn request);
        string Register(LoginSingOn request);
    }
}
