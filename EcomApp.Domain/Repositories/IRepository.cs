using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApp.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        T GetById(Guid id);
        Task<T> GetByIdAsync(Guid id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
