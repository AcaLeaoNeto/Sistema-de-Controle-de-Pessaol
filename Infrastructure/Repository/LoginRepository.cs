using Domain.Entitys.Base;
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

        public LoginRepository(DBContext db, INotification notification) : base(db)
        {
            _db = db;
            _Notification = notification;
        }


        public  bool AnyLog(string username)
        {     
            return  _db.Logs.Any(l => l.Username == username);
        }


        public Log? GetLogByUsername(string username)
        {
            return _db.Logs.FirstOrDefault(l => l.Username == username);
        }


        public BaseResponse RegisterLog(Log LogForm)
        {
            try
            {
                Insert(LogForm);
            }
            catch (Exception)
            {
                _Notification.AddMessage("Formulario invalido");
                return new BaseResponse(404, "Erro");
            }
            
            return new BaseResponse();
        }


    }
}
