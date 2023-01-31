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

        public async Task<bool> DesativarUsuario(int id)
        {
            var user = _db.usuarios.FirstOrDefault(u => u.Id == id && u.Ativo == true);
            if (user is null)
                return false;

            user.Ativo = false;
            await _db.SaveChangesAsync();

            return true;
        }
    

        public async Task<List<User>?> ApagarUsuario(int id)
        {
            var user = _db.usuarios.FirstOrDefault(u => u.Id == id);
            if (user is null)
                return null;

            Delete(user);

            return await UsuariosAtivos();
        }

        public async Task<List<User>> UsuariosAtivos()
        {
            var users = await _db.usuarios.Where(u => u.Ativo == true).ToListAsync();
            return users;
        }

        public User UsuarioById(int id)
        {
            var user = _db.usuarios.FirstOrDefault(u => u.Id == id && u.Ativo == true);
            if (user is null)
                return null;

            return user;
        }

        public async Task<List<User>> Cadastro(UserDto obj)
        {
            var user = (User)obj;

            if(user.Validation(_notification))
                Insert(user);

            return await UsuariosAtivos();
        }

        public async Task<List<User>> Alterar(User obj)
        {

            if (obj.Validation(_notification))
                Update(obj);

            return await UsuariosAtivos();
        }
    }
}
