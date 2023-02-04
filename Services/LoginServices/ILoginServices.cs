using Domain.Entitys.Login;
using System.Security.Claims;

namespace Services.LoginServices
{
    public interface ILoginServices
    {
        LogResponse Login(SingIn request);
        object Register(SingOn request);
        LogResponse RefreshAcess(string acess, IEnumerable<Claim> refresh);
    }
}
