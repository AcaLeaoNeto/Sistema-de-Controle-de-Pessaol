
namespace Domain.Interfaces
{
    public interface IBase<T> where T : class
    {
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
        List<T> GetAll();
        T GetById(int id);
    }
}
