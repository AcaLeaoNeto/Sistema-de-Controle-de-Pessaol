using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Domain.Notifications;
using Domain.Entitys.Usuario;
using Microsoft.AspNetCore.Authorization;

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
            public async Task<ActionResult<List<User>>> GetAllUsuarios()
            {
                try
                {
                    var AllUsers = await _usuario.UsuariosAtivos();
                    return Ok(AllUsers);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpGet("{id}")]
            public ActionResult<List<User>> GetUsuario([FromBody] int id)
            {
                try
                {
                    var user = _usuario.UsuarioById(id);
                    if (user is null)
                        return NotFound("Usuario Não Encontrado");

                    return Ok(user);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        [HttpPost]
            public async Task<ActionResult<List<User>>> AddUsuario([FromBody] UserDto user)
            {
                if (!ModelState.IsValid)
                    return BadRequest();

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
            public async Task<ActionResult<User>> AlterarUsuario([FromBody] User user)
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

            [HttpDelete("{id}"), Authorize(Roles = "Manager")]
            public async Task<ActionResult<User>> DeletarUsuario([FromBody] int id)
            {
                try
                {
                    var users = await _usuario.ApagarUsuario(id);
                    if (users is null)
                        return NotFound("Usuario Não Encontrado");

                    return Ok(users);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpPatch("Desativar/{id}"), Authorize(Roles = "Manager")]
            public async Task<ActionResult<User>> DesativarUsuario([FromBody] int id)
            {
                try
                {
                    var result = await _usuario.DesativarUsuario(id);
                    if (result == false)
                        return NotFound("Usuario Não Encontrado");

                    return Ok($"Usuario Id:{id} Desativado.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

        }
    }

