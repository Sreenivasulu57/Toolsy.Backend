using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VSC.Toolsy.Repositories.Data;
using VSC.Toolsy.Repositories.Interfaces;

namespace VSC.Toolsy.Repositories
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
        
        //To Save The Object
        public T Save(T entity)
            => DbSet.Add(entity).Entity;

        //To Save The Object
        public async Task<T> SaveAsync(T entity)
        {
            EntityEntry<T> entityFromDb = await DbSet.AddAsync(entity);
            return entityFromDb.Entity;
        }

        //To Get All Objects From Db
        public List<T> GetAll()
            => DbSet.ToList();

        //To Get All Objects From Db
        public async Task<List<T>> GetAllAsync()
            => await DbSet.ToListAsync();

        //To For Custom Repository Methods
        public IQueryable<T> Query()
           => DbSet;

        //To Persist In The Db
        public int SaveChanges()
           => Context.SaveChanges();

        public async Task<int> SaveChangesAsync()
           => await Context.SaveChangesAsync();

        //To Check The Entity State
        public DbContext GetContext()
            => Context;

        public void Update(T entity)
            => DbSet.Update(entity);

        public void Delete(T entity) 
            => DbSet.Remove(entity);

    }
}
