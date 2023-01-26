using Domain.Entitys.Login;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository
{
    public class LoginRepository : BaseRepository<Login>, ILogin
    {
        private readonly DBContext _db;

        public LoginRepository(DBContext db) : base(db)
        {
            _db = db;
        }

        public Task<string> registerLog(LoginDto LogForm)
        {
            throw new NotImplementedException();
        }
    }
}
