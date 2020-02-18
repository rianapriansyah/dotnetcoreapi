using System.Collections.Generic;

namespace stargate.Repository{
    public interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity, int id);
        void Delete(T entity);
    }
}