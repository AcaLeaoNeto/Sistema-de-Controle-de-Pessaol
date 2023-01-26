using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class BaseRepository<T> : ILogin<T> where T : class
    {
        private readonly DBContext _db;

        public BaseRepository(DBContext db)
        {
            _db = db;
        }

        public void Delete(T obj)
        {
            _db.Set<T>().Remove(obj);
            _db.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _db.Set<T>().Find(id);
        }

        public void Insert(T obj)
        {
            _db.Set<T>().Add(obj);
            _db.SaveChanges();
        }

        public void Update(T obj)
        {
            _db.Entry(obj).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
