using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Domain.Notifications;
using Domain.Entitys.Usuario;
using Microsoft.AspNetCore.Authorization;
using Domain.Entitys.Base;

namespace Presentation.Controllers
{
        [Route("api/[controller]")]
        [ApiController, Authorize]
        public class UsuarioController : ControllerBase
        {
        private readonly IUsuario _usuario;
        private readonly INotification _notification;

            public UsuarioController(IUsuario usuario, INotification notification)
            {
                _usuario = usuario;
                _notification = notification;
            }

            [HttpGet]
            public async Task<ActionResult<BaseResponse>> GetAllUsuarios()
            {
                try
                {
                    return BaseOperation(await _usuario.UsuariosAtivos());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpGet("Desativados")]
            public async Task<ActionResult<BaseResponse>> GetDesativosUsuarios()
            {
                try
                {
                    return BaseOperation(await _usuario.UsuariosDesativos());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<BaseResponse>> GetUsuarioAsync(int id)
            {
                try 
                { 
                    return BaseOperation(await _usuario.UsuarioById(id));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpPost]
            public async Task<ActionResult<BaseResponse>> AddUsuario(UserRequest user)
            {
                try
                {
                    return BaseOperation(await _usuario.Cadastro(user));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpPut]
            public async Task<ActionResult<BaseResponse>> AlterarUsuario(UserChange user)
            {
                try
                {
                    return BaseOperation(await _usuario.Alterar(user));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult<BaseResponse>> DeletarUsuario(int id)
            {
                try
                {
                    return BaseOperation(await _usuario.ApagarUsuario(id));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpPatch("Desativar/id")]
            public async Task<ActionResult<BaseResponse>> DesativarUsuario(int id)
            {
                try
                {
                    return BaseOperation(await _usuario.DesativarUsuario(id));
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            private ActionResult<BaseResponse> BaseOperation(BaseResponse obj)
            {
                if (_notification.Valid)
                    return Ok(obj);

                obj.ResponseMessage.RemoveAt(0);
                foreach(var message in _notification.Messages)
                    obj.ResponseMessage.Add(message);

                obj.ResponseObject = null;
                return StatusCode(obj.StatusCode, obj);
            }

        }
    }

