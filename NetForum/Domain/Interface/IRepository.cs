using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T Get(T entity);
        Task<T> GetAsync(T entity);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
    }
}
