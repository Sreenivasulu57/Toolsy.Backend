using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSC.Toolsy.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        public T Save(T TEntity);

        public Task<T> SaveAsync(T TEntity);

        public List<T> GetAll();

        public Task<List<T>> GetAllAsync();

        public IQueryable<T> Query();

        public int SaveChanges();

        public Task<int> SaveChangesAsync();

        public DbContext GetContext();
        public void Update(T entity);

        public void Delete(T entity);
    }
}
