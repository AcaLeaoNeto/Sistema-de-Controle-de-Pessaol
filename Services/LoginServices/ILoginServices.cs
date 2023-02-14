using Domain.Entitys.Base;
using Domain.Entitys.Login;
using System.Security.Claims;

namespace Services.LoginServices
{
    public interface ILoginServices
    {
        BaseResponse Login(SingIn request);
        BaseResponse Register(SingOn request);
        BaseResponse RefreshAcess(string acess, IEnumerable<Claim> refresh);
    }
}
