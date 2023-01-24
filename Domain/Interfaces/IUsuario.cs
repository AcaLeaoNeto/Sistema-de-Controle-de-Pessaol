using Domain.Entitys;

namespace Domain.Interfaces
{
    public interface IUsuario : IBaseInterface<Usuario>
    {
        Task<bool> DesativarUsuario(int id);
        Task<List<Usuario>?> ApagarUsuario(int id);
        Task<List<Usuario>> UsuariosAtivos();
        Usuario UsuarioById(int id);
        Task<List<Usuario>> Cadastro(UsuarioDto obj);
        Task<List<Usuario>> Alterar(Usuario obj);
    }
}
