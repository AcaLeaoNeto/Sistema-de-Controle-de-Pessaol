using Domain.Entitys.Base;
using Domain.Entitys.Login;
using System.Security.Claims;

namespace Services.LoginServices
{
    public interface ILoginServices
    {
        SingInResponse Login(SingIn request);
        BaseResponse Register(SingOn request);
        SingInResponse RefreshAcess(string acess, IEnumerable<Claim> refresh);
    }
}
