using Domain.Entitys;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UsuarioRepository : BaseRepository<Usuario> , IUsuario
    {
        private readonly DBContext _db;

        public UsuarioRepository(DBContext db) : base(db)
        {
            _db = db;
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
    

        public async Task<List<Usuario>?> ApagarUsuario(int id)
        {
            var user = _db.usuarios.FirstOrDefault(u => u.Id == id);
            if (user is null)
                return null;

            Delete(user);

            return await UsuariosAtivos();
        }

        public async Task<List<Usuario>> UsuariosAtivos()
        {
            var users = await _db.usuarios.Where(u => u.Ativo == true).ToListAsync();
            return users;
        }

        public Usuario UsuarioById(int id)
        {
            var user = _db.usuarios.FirstOrDefault(u => u.Id == id && u.Ativo == true);
            if (user is null)
                return null;

            return user;
        }

        public async Task<List<Usuario>> Cadastro(Usuario obj)
        {
            if (obj is null)
                return null;

            if (!obj.ValidarData())
                return null;

            if (!obj.ValidarIdade())
                return null;

            Insert(obj);

            return await UsuariosAtivos();
        }

        public async Task<List<Usuario>> Alterar(Usuario obj)
        {
            if (obj is null || !obj.ValidarData() || !obj.ValidarIdade())
                return null;

            Update(obj);

            return await UsuariosAtivos();
        }
    }
}
