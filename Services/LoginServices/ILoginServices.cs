using Domain.Entitys.Login;
using Microsoft.AspNetCore.Mvc;

namespace Services.LoginServices
{
    public interface ILoginServices
    {
        string Login(SingIn request);
        string Register(SingOn request);
    }
}
