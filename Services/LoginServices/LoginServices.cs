using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Domain.Entitys.Login;
using Domain.Interfaces;
using Domain.Notifications;

namespace Services.LoginServices
{
    public class LoginServices : ILoginServices
    {

        private readonly IConfiguration _configuration;
        private readonly ILogin _login;
        private readonly INotification _notification;

        public LoginServices(IConfiguration configuration, ILogin login,INotification notification)
        {
            _configuration = configuration;
            _login = login;
            _notification = notification;
        }



        public object Register(SingOn request)
        {
            if (_login.AnyLog(request.Username))
            {
                _notification.AddMessage("Log já Cadastrado");
                return null;
            }
            
            var userGuid = _login.GetUserId(request.UserId);
            
            if (_notification.Valid)
            {
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var Newlog = new Log(request.Username, passwordHash, passwordSalt, request.Role, userGuid);
                var result = _login.RegisterLog(Newlog);

                return result;
            }

            return null;
        }


        public LogResponse Login(SingIn request)
        {

            var TryLog = _login.GetLogByUsername(request.Username);

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
                var response = CreateToken( new List<Claim> {
                        new Claim(ClaimTypes.Name, TryLog.Username),
                        new Claim(ClaimTypes.Role, TryLog.Role) } );

                return response;
            }

            return null;
        }


        public LogResponse RefreshAcess(string acess, IEnumerable<Claim> refresh)
        {
            var AtSplit = acess.Split(".");
            var AcessTk = new JwtSecurityTokenHandler().ReadJwtToken(acess);

            if(AcessTk.ValidTo < DateTime.Now)
            {
                _notification.AddMessage("Token Incorreto");
                return null;
            }
                
            var ExpAt = AcessTk.Claims.First(x => x.Type == "exp").Value;
            var Atk = refresh.First(x => x.Type == "Atk").Value;
            var Ate = refresh.First(x => x.Type == "Ate").Value;

            if(Ate != ExpAt || AtSplit[2] != Atk)
            {
                _notification.AddMessage("Token Ivalido");
                return null;
            }

            var userClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,
                        AcessTk.Claims.First(x => x.Type == ClaimTypes.Name).Value),

                    new Claim(ClaimTypes.Role,
                        AcessTk.Claims.First(x => x.Type == ClaimTypes.Role).Value)
                };

            var response = CreateToken(userClaims);
            return response;
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



        private LogResponse CreateToken(List<Claim> userClaims)
        {            
            
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("Setting:TKPrivate").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);


            var AcessClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userClaims.First(uc => uc.Type == ClaimTypes.Name).Value),
                new Claim(ClaimTypes.Role, userClaims.First(uc => uc.Type == ClaimTypes.Role).Value)
            };

            var Acess = new JwtSecurityToken(
                claims: AcessClaims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            var AcessToken = new JwtSecurityTokenHandler().WriteToken(Acess);



            List<Claim> RefreshClaims = new List<Claim>
            {
                new Claim("Atk", AcessToken.Split('.')[2]),
                new Claim("Ate", Acess.Claims.First(a => a.Type =="exp").Value)
            };

            var Refresh = new JwtSecurityToken(
                claims: RefreshClaims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds);

            var RefreshToken = new JwtSecurityTokenHandler().WriteToken(Refresh);



            return new LogResponse(AcessToken, RefreshToken);
        }
    }
}
