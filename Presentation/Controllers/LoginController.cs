using Microsoft.AspNetCore.Mvc;
using Services.LoginServices;
using Domain.Entitys.Login;
using Domain.Notifications;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginServices _Login;
        private readonly INotification _Notification;

        public LoginController(ILoginServices login, INotification notification)
        {
            _Login = login;
            _Notification = notification;
        }


        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(LoginDto request)
        {
            if (request is null)
                return BadRequest("Formulario em branco");

            try
            {
                var result = _Login.Register(request);

                if (_Notification.Valid)
                    return Ok(result);
                else
                    return BadRequest(_Notification.Messages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
            

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto request)
        {
            try
            {
                var KeyTK = _Login.Login(request);
                return Ok(KeyTK);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
