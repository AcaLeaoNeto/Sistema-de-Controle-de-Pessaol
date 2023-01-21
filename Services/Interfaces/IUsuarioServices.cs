using Domain.Entitys;
using Domain.Entitys.Dtos;

namespace Services.Interfaces
{
    public interface IUsuarioServices : IBaseServices<Usuario>
    {
        Task<bool> DesativarUsuario(int id);
        Task<List<Usuario>?> ApagarUsuario(int id);
        Task<List<Usuario>> UsuariosAtivos();
        Usuario UsuarioById(int id);
        Task<List<Usuario>> Cadastro(UsuarioDTO obj);
        Task<List<Usuario>> Alterar(Usuario obj);
    }
}
