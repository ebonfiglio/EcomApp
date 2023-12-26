using EcomApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApp.Infrastructure.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly EcomAppDbContext Context;
        private DbSet<T> entities;

        public GenericRepository(EcomAppDbContext context)
        {
            Context = context;
            entities = context.Set<T>();
        }

        public T GetById(Guid id)
        {
            return entities.Find(id);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await entities.FindAsync(id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public void Add(T entity)
        {
            entities.Add(entity);
        }

        public void Update(T entity)
        {
            entities.Update(entity);
        }

        public void Delete(T entity)
        {
            entities.Remove(entity);
        }
    }
}