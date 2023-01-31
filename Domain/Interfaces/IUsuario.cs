using Domain.Entitys.Usuario;

namespace Domain.Interfaces
{
    public interface IUsuario : IBase<User>
    {
        Task<bool> DesativarUsuario(int id);
        Task<List<User>?> ApagarUsuario(int id);
        Task<List<User>> UsuariosAtivos();
        User UsuarioById(int id);
        Task<List<User>> Cadastro(UserDto obj);
        Task<List<User>> Alterar(User obj);
    }
}
