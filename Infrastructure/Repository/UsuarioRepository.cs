using Domain.Entitys.Base;
using Domain.Entitys.Usuario;
using Domain.Interfaces;
using Domain.Notifications;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UsuarioRepository : BaseRepository<User> , IUsuario
    {
        private readonly DBContext _db;
        private readonly INotification _notification;


        public UsuarioRepository(DBContext db, INotification notification) : base(db)
        {
            _db = db;
            _notification = notification;
        }

        public async Task<BaseResponse> DesativarUsuario(int id)
        {
            var user = _db.usuarios.FirstOrDefault(u => u.CodigoUsuario == id && u.Ativo == true);

            if (user is null)
            {
                _notification.AddMessage("Usuario não encontrado");
                return new BaseResponse(404, "Erro");
            }

            user.Ativo = false;
            await _db.SaveChangesAsync();

            return new BaseResponse();
        }
    

        public async Task<BaseResponse> ApagarUsuario(int id)
        {
            var user = _db.usuarios.FirstOrDefault(u => u.CodigoUsuario == id);
            if (user is null)
                return null;

            Delete(user);

            return new BaseResponse();
        }

        public async Task<BaseResponse> UsuariosAtivos()
        {
            var users = await _db.usuarios.Where(u => u.Ativo == true).ToListAsync();

            return new BaseResponse(responseObject: users);
        }

        public async Task<BaseResponse> UsuariosDesativos()
        {
            var users = await _db.usuarios.Where(u => u.Ativo == false).ToListAsync();

            var response = new BaseResponse(responseObject: users);
            response.ResponseObject = users;
            return response;
        }

        public async Task<BaseResponse> UsuarioById(int id)
        {
            var user = await  _db.usuarios.FirstOrDefaultAsync(u => u.CodigoUsuario == id && u.Ativo == true);
            if (user is null)
                return null;

            var response = new BaseResponse(responseObject: user);
            response.ResponseObject = user;
            return response;
        }

        public async Task<BaseResponse> Cadastro(UserDto obj)
        {
            try
            {
                var user = (User)obj;
                Insert(user);

                return new BaseResponse();
            }
            catch (Exception)
            {
                _notification.AddMessage("Erro na execução");
                return null;
            }
        }

        public async Task<BaseResponse> Alterar(User obj)
        {
            try
            {
                Update(obj);

                return new BaseResponse();
            }
            catch (Exception)
            {
                _notification.AddMessage("Erro na execução");
                return null;
            }
        }
    }
}
