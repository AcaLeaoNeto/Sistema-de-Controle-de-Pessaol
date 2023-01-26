using Microsoft.AspNetCore.Mvc;
using Services.LoginServices;
using Domain.Entitys.Login;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginServices _Login;

        public LoginController(ILoginServices login)
        {
            _Login = login;
        }


        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(LoginDto request)
        {
            var result = _Login.Register(request);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto request)
        { 
            return  Ok(_Login.Login(request));
        }
    }
}
