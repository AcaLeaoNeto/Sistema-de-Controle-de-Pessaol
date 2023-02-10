namespace Domain.Entitys.Login
{
    public class SingInResponse
    {
        public SingInResponse(string acessToken, string refreshToken)
        {
            AcessToken = acessToken;
            RefreshToken = refreshToken;
        }


        public String AcessToken { get; } = string.Empty;
        public String RefreshToken { get; } = string.Empty;
    }
}
