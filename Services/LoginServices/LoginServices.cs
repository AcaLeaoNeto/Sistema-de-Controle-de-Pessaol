using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Domain.Entitys.Login;
using Domain.Interfaces;
using Domain.Notifications;
using Domain.Entitys.Usuario;
using Infrastructure.Context;

namespace Services.LoginServices
{
    public class LoginServices : ILoginServices
    {

        private readonly IConfiguration _configuration;
        private readonly ILogin _login;
        private readonly DBContext _db;
        private readonly INotification _notification;


        public LoginServices(IConfiguration configuration, ILogin login, INotification notification, DBContext db)
        {
            _configuration = configuration;
            _login = login;
            _notification = notification;
            _db = db;
        }



        public string Register(SingOn request)
        {
            if (_login.GetByUsername(request.Username) is not null)
                _notification.AddMessage("Log já Cadastrado");

            if (request.Validation(_notification))
            {
                var user = _login.GetUserId(request.UserId);

                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var Newlog = new Log(request.Username, passwordHash, passwordSalt, request.Role, user.Id);
                var result = _login.RegisterLog(Newlog);

                return result;
            }

            return "Erro";
        }



        public string Login(SingIn request)
        {

            var TryLog = _login.GetByUsername(request.Username);

            if (TryLog is null)
            {
                _notification.AddMessage("Log Não Encontrado");
            }
            else if (!VerifyPasswordHash(request.Password, TryLog.PasswordHash, TryLog.PasswordSalt))
            {
                _notification.AddMessage("Senha Incorreta");
            }
                

            if (_notification.Valid)
            {
                var token = CreateToken(TryLog);
                return token;
            }

            return "Erro";
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



        private string CreateToken(Log user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
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
