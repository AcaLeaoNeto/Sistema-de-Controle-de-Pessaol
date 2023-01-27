using Microsoft.AspNetCore.Mvc;
using Services.LoginServices;
using Domain.Entitys.Login;
using Domain.Notifications;
using Microsoft.AspNetCore.Authorization;
using System.Data;

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


        [HttpPost("register"), Authorize(Roles = "Manager")]
        public async Task<ActionResult<string>> Register(LoginSingOn request)
        {
            if (request is null)
                return BadRequest("Formulario em branco");

            try
            {
                var result = _Login.Register(request);

                if (_Notification.Valid)
                    return result;
                else
                    return BadRequest(_Notification.Messages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
            

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginSingIn request)
        {
            try
            {
                var response = _Login.Login(request);
                if (_Notification.Valid)
                    return response; 
                else
                    return BadRequest(_Notification.Messages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
