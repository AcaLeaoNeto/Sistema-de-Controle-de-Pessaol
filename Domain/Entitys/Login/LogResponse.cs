
namespace Domain.Entitys.Login
{
    public class LogResponse
    {
        public LogResponse(string acessToken, string refreshToken)
        {
            AcessToken = acessToken;
            RefreshToken = refreshToken;
        }


        public String AcessToken { get; } = string.Empty;
        public String RefreshToken { get;} = string.Empty;
    }
}
