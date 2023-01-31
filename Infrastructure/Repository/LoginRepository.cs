﻿using Domain.Entitys.Login;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository
{
    public class LoginRepository : BaseRepository<Log>, ILogin
    {
        private readonly DBContext _db;

        public LoginRepository(DBContext db) : base(db)
        {
            _db = db;
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
            Insert(LogForm);
            return "Log Registrado";
        }


    }
}