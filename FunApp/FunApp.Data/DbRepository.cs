using FunApp.Data.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FunApp.Data
{
    public class DbRepository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {
        private readonly FunAppDbContext context;
        private readonly DbSet<TEntity> dbSet;
             
        public DbRepository(FunAppDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public Task AddAsync(TEntity entity)
        {
            return dbSet.AddAsync(entity);
        }

        public IQueryable<TEntity> All()
        {
            return dbSet;
        }

        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
    }
}
