using Microsoft.AspNetCore.Mvc;
using Services.LoginServices;
using Domain.Entitys.Login;
using Domain.Notifications;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Domain.Entitys.Base;

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
        public async Task<ActionResult<BaseResponse>> Register([FromBody] SingOn request)
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
        public async Task<ActionResult<SingInResponse>> Login([FromBody] SingIn request)
        {

            try
            {
                var response = _Login.Login(request);
                if (_Notification.Valid)
                    return Ok(response); 
                else
                    return BadRequest(_Notification.Messages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("RefreshLogin"), Authorize]
        public async Task<ActionResult<SingInResponse>> RefeshLogin([FromBody] string request)
        {

            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var response = _Login.RefreshAcess(request, identity.Claims);
                if (_Notification.Valid)
                    return Ok(response);
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
