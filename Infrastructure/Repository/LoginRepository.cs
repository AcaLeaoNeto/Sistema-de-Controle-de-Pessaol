using Domain.Entitys.Login;
using Domain.Entitys.Usuario;
using Domain.Interfaces;
using Domain.Notifications;
using Infrastructure.Context;

namespace Infrastructure.Repository
{
    public class LoginRepository : BaseRepository<Log>, ILogin
    {
        private readonly DBContext _db;
        private readonly INotification _Notification;

        public LoginRepository(DBContext db, INotification notification, IUsuario usuario) : base(db)
        {
            _db = db;
            _Notification = notification;
        }

        public  User? GetUserId(int id)
        {
            return  _db.usuarios.FirstOrDefault(u => u.CodigoUsuario == id);
        }

        public Log GetByUsername(string username)
        {
            var TryLog =  _db.Logs.FirstOrDefault(l => l.Username == username);
            if (TryLog is null)
                return null;     

            return TryLog;
        }

        public string RegisterLog(Log LogForm)
        {
            try
            {
                Insert(LogForm);
            }
            catch (Exception)
            {
                _Notification.AddMessage("Formulario invalido");
            }
            
            return "Log Registrado";
        }


    }
}
