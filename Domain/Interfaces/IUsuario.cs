using Domain.Entitys.Base;
using Domain.Entitys.Usuario;

namespace Domain.Interfaces
{
    public interface IUsuario : IBase<User>
    {
        Task<BaseResponse> DesativarUsuario(int id);
        Task<BaseResponse> ApagarUsuario(int id);
        Task<BaseResponse> UsuariosAtivos();
        Task<BaseResponse> UsuariosDesativos();
        Task<BaseResponse> UsuarioById(int id);
        Task<BaseResponse> Cadastro(UserDto obj);
        Task<BaseResponse> Alterar(UserChange obj);
    }
}
