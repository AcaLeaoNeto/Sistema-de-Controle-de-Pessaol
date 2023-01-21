using Services.Interfaces;
using Domain.Interfaces;

namespace Services.Services
{
    public class BaseServices<T> : IBaseServices<T> where T : class
    {
        private readonly IBaseInterface<T> _IBase;

        public BaseServices(IBaseInterface<T> iBase)
        {
            _IBase = iBase;
        }

        public void Delete(T obj)
        {
            _IBase.Delete(obj);
        }

        public List<T> GetAll()
        {
            return _IBase.GetAll();
        }

        public T GetById(int id)
        {
            return _IBase.GetById(id);
        }

        public void Insert(T obj)
        {
            _IBase.Insert(obj);
        }

        public void Update(T obj)
        {
            _IBase.Update(obj);
        }
    }
}
