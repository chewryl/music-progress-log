using Microsoft.EntityFrameworkCore;
using MusicProgressLogAPI.Data;
using MusicProgressLogAPI.Repositories.Interfaces;

namespace MusicProgressLogAPI.Repositories
{
    public class SqlRepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<T> _dbSet;

        public SqlRepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual async Task<T> CreateAsync(T obj)
        {
            await _dbSet.AddAsync(obj);
            await _dbContext.SaveChangesAsync();
            // Changes made to the obj will persist, since EFCore will track the provided entity.
            return obj;
        }

        public virtual async Task<Guid?> DeleteAsync(Guid id)
        {
            var obj = await _dbSet.FindAsync(id);
            if (obj == null)
            {
                return null;
            }
            
            _dbSet.Remove(obj);
            await _dbContext.SaveChangesAsync();
            return id;
        }
        
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
           return await _dbSet.FindAsync(id);
        }

        public virtual void UpdateAsync(Guid id, T obj)
        {
            // Make entity state modified.
            _dbSet.Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
