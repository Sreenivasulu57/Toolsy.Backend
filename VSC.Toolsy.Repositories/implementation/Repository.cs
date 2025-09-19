using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Repositories.implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext Context { get; }

        protected DbSet<T> DbSet { get; }

        public Repository(ApplicationDbContext context)
        {
            Console.WriteLine("Repository initized in DI");
            Context = context;
            DbSet = Context.Set<T>();
        }

        public T Save(T entity)
            => DbSet.Add(entity).Entity;

        public async Task<T> SaveAsync(T entity)
        {
            EntityEntry<T> entityFromDb = await DbSet.AddAsync(entity);
            return entityFromDb.Entity;
        }

        public List<T> GetAll()
            => DbSet.ToList();

        public async Task<List<T>> GetAllAsync()
            => await DbSet.ToListAsync();

        public IQueryable<T> Query()
           => DbSet;

        public int SaveChanges()
           => Context.SaveChanges();

        public async Task<int> SaveChangesAsync()
           => await Context.SaveChangesAsync();

        public DbContext GetContext()
            => Context;

        public void Update(T entity)
            => DbSet.Update(entity);

        public void Delete(T entity)
            => DbSet.Remove(entity);

    }
}
