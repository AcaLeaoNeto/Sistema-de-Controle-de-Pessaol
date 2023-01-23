using Domain.Entitys;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Domain.Notifications;

namespace Presentation.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
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
            public async Task<ActionResult<List<Usuario>>> GetAllUsuarios()
            {
                return await _usuario.UsuariosAtivos();
            }

            [HttpGet("{id}")]
            public ActionResult<List<Usuario>> GetUsuario(int id)
            {
                var user = _usuario.UsuarioById(id);
                if (user is null)
                    return NotFound("Usuario Não Encontrado");

                return Ok(user);
            }

        [HttpPost]
            public async Task<ActionResult<List<Usuario>>> AddUsuario(Usuario user)
            {
                try
                {
                    var users = await _usuario.Cadastro(user);
                    if (_notification.Valid)
                        return Ok(users); 
                    else
                        return BadRequest(_notification.Messages);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpPut]
            public async Task<ActionResult<Usuario>> AlterarUsuario(Usuario user)
            {
                try
                {
                    var users = await _usuario.Alterar(user);
                    if (_notification.Valid)
                        return Ok(users);
                    else
                        return BadRequest(_notification.Messages);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult<Usuario>> DeletarUsuario(int id)
            {
                var users = await _usuario.ApagarUsuario(id);
                if (users is null)
                    return NotFound("Usuario Não Encontrado");

                return Ok(users);
            }

            [HttpPatch("Desativar/{id}")]
            public async Task<ActionResult<Usuario>> DesativarUsuario(int id)
            {
                var result = await _usuario.DesativarUsuario(id);
                if (result == false)
                    return NotFound("Usuario Não Encontrado");

                return Ok();
            }

        }
    }

