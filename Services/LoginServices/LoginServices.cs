using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Domain.Entitys.Login;
using Domain.Interfaces;
using Domain.Notifications;
using Domain.Entitys.Base;

namespace Services.LoginServices
{
    public class LoginServices : ILoginServices
    {

        private readonly IConfiguration _configuration;
        private readonly ILogin _loginRepository;
        private readonly INotification _notification;

        public LoginServices(IConfiguration configuration, ILogin login,INotification notification)
        {
            _configuration = configuration;
            _loginRepository = login;
            _notification = notification;
        }



        public BaseResponse Register(SingOn request)
        {
            if (_loginRepository.AnyLog(request.Username))
            {
                _notification.AddMessage("Log já Cadastrado");
                return new BaseResponse(404, "Erro");
            }


            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var Newlog = new Log(request.Username, passwordHash, passwordSalt, request.Role, request.UserId);
            var result = _loginRepository.RegisterLog(Newlog);

            return result;
            
        }


        public BaseResponse Login(SingInRequest request)
        {

            var TryLog = _loginRepository.GetLogByUsername(request.Username);

            if (TryLog is null || !VerifyPasswordHash(request.Password, TryLog.PasswordHash, TryLog.PasswordSalt))
            {
                _notification.AddMessage("Usuario ou Senha Incorreta.");
                return new BaseResponse(400, "Erro");
            }

			var response = CreateToken( new List<Claim> {
                new Claim(ClaimTypes.Name, TryLog.Username),
                new Claim(ClaimTypes.Role, TryLog.Role) 
            });

            return new BaseResponse(responseObject: response);
        }


        public BaseResponse RefreshAcess(string acess, IEnumerable<Claim> refresh)
        {
            
            var AcessTk = new JwtSecurityTokenHandler().ReadJwtToken(acess);
            
            if(AcessTk.ValidTo > DateTime.UtcNow)
            {
                _notification.AddMessage("Token Ainda Valido");
                return new BaseResponse(400, "Erro");
            }

            var AtSplit = acess.Split(".");

            var ExpAt = AcessTk.Claims.First(x => x.Type == "exp").Value;
            var Atk = refresh.First(x => x.Type == "Atk").Value;
            var Ate = refresh.First(x => x.Type == "Ate").Value;

            if(Ate != ExpAt || AtSplit[2] != Atk)
            {
                _notification.AddMessage("Token Ivalido");
                return new BaseResponse(400, "Erro");
            }

            var userClaims = new List<Claim>
            { 
                new Claim(ClaimTypes.Name,
                AcessTk.Claims.First(x => x.Type == ClaimTypes.Name).Value),

                new Claim(ClaimTypes.Role,
                AcessTk.Claims.First(x => x.Type == ClaimTypes.Role).Value)
             };

            var response = CreateToken(userClaims);
            return new BaseResponse(responseObject: response);
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



        private SingInResponse CreateToken(List<Claim> userClaims)
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
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: creds);

            var AcessToken = new JwtSecurityTokenHandler().WriteToken(Acess);



            List<Claim> RefreshClaims = new List<Claim>
            {
                new Claim("Atk", AcessToken.Split('.')[2]),
                new Claim("Ate", Acess.Claims.First(a => a.Type =="exp").Value)
            };

            var Refresh = new JwtSecurityToken(
                claims: RefreshClaims,
                expires: DateTime.Now.AddMinutes(3),
                signingCredentials: creds);

            var RefreshToken = new JwtSecurityTokenHandler().WriteToken(Refresh);

            return new SingInResponse(AcessToken, RefreshToken);
        }
    }
}
