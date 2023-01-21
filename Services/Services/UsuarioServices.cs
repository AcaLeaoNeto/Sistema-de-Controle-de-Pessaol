using AutoMapper;
using Domain.Entitys;
using Domain.Entitys.Dtos;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services.Services
{
    public class UsuarioServices : BaseServices<Usuario>, IUsuarioServices
    {
        private readonly IUsuario _Usuario;
        private readonly IMapper _Mapper;

        public UsuarioServices(IUsuario Usuario, IMapper mapper) : base(Usuario)
        {
            _Usuario = Usuario;
            _Mapper = mapper;
        }

        public Task<List<Usuario>> Alterar(Usuario obj)
        {
           return _Usuario.Alterar(obj);
        }

        public Task<List<Usuario>?> ApagarUsuario(int id)
        {
            return _Usuario.ApagarUsuario(id);
        }

        public Task<List<Usuario>> Cadastro(UsuarioDTO obj)
        {
            var user = _Mapper.Map<Usuario>(obj);
            return _Usuario.Cadastro(user);
        }

        public Task<bool> DesativarUsuario(int id)
        {
            return _Usuario.DesativarUsuario(id);
        }

        public Usuario UsuarioById(int id)
        {
            return _Usuario.GetById(id);
        }

        public Task<List<Usuario>> UsuariosAtivos()
        {
            return _Usuario.UsuariosAtivos();
        }
    }
}
