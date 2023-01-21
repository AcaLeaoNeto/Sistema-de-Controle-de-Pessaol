using Domain.Entitys;
using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;

namespace Presentation.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class UsuarioController : ControllerBase
        {
        private readonly IUsuario _usuario;

            public UsuarioController(IUsuario usuario)
            {
                _usuario = usuario;
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
                var users = await _usuario.Cadastro(user);
                if (users is null)
                    return NotFound("Usuario Não Encontrado");

                return Ok(users);
            }

            [HttpPut]
            public async Task<ActionResult<Usuario>> AlterarUsuario(Usuario user)
            {
                var newUser = await _usuario.Alterar(user);
                if (newUser is null)
                    return NotFound("Usuario Não Encontrado");

                return Ok(newUser);
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

