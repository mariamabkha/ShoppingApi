using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ShoppingApi.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ShoppingDBContext _dbContext;


        //Injection
        public GenericRepository(ShoppingDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null,
            Expression<Func<T, object>>[] includes = null)
        {
            var data = expression == null ? _dbContext.Set<T>().AsQueryable() : _dbContext.Set<T>().Where(expression);

            if (includes != null)
            {
                data = includes.Aggregate(data, (item, include) => item.Include(include));
            }

            return await data.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> expression = null,
            Expression<Func<T, object>>[] includes = null)
        {
            var data = expression == null ? _dbContext.Set<T>().AsQueryable() : _dbContext.Set<T>().Where(expression);

            if (includes != null)
            {
                data = includes.Aggregate(data, (item, include) => item.Include(include));
            }

            return await data.FirstOrDefaultAsync();
        }

        public async Task AddAsync(T obj)
        {
            await _dbContext.Set<T>().AddAsync(obj);
            _dbContext.Entry(obj).State = EntityState.Added;
        }

        public void Update(T obj)
        {
            _dbContext.Set<T>().Update(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entity = _dbContext.Set<T>().Find(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
