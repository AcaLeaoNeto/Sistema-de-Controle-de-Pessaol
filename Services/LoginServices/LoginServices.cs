using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Domain.Entitys.Login;
using Domain.Interfaces;


namespace Services.LoginServices
{
    public class LoginServices : ILoginServices
    {

        private readonly IConfiguration _configuration;
        private readonly ILogin _login;



        public LoginServices(IConfiguration configuration, ILogin login)
        {
            _configuration = configuration;
            _login = login;
        }


        public string Register(LoginDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var Newlog = new Login(request.Username, passwordHash, passwordSalt);

            var result  = _login.RegisterLog(Newlog);

            return result;
        }


        public string Login(LoginDto request)
        {
            //if (user.Username != request.Username)
            //{
            //    return "User not found.";
            //}

            var TryLog = _login.GetByUsername(request.Username);


            if (!VerifyPasswordHash(request.Password, TryLog.PasswordHash, TryLog.PasswordSalt))
            {
                return "Wrong password.";
            }

            string token = CreateToken(TryLog);

            return token;
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }


        private string CreateToken(Login user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Setting:TKPrivate").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


    }
}
