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


        [HttpPost("register"), Authorize]
        public async Task<ActionResult<BaseResponse>> Register(SingOn request)
        {

            if (request is null)
                return BadRequest("Formulario em branco");

            try
            {
                return BaseOperation(_Login.Register(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost("login")]
        public async Task<ActionResult<BaseResponse>> Login(SingInRequest request)
        {
            try
            {
                return BaseOperation(_Login.Login(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("RefreshLogin"), Authorize]
        public async Task<ActionResult<BaseResponse>> RefeshLogin([FromBody] string request)
        {

            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                return BaseOperation(_Login.RefreshAcess(request, identity.Claims));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private ActionResult<BaseResponse> BaseOperation(BaseResponse obj)
        {
            if (_Notification.Valid)
                return Ok(obj);

            obj.ResponseObject = _Notification.Messages;
            return StatusCode(obj.StatusCode, obj);
        }
    }

}
