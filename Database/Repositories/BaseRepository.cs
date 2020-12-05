using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _applicationContext;
        private DbSet<T> _set;

        public BaseRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _set = applicationContext.Set<T>();
        }

        async Task<bool> IBaseRepository<T>.ExistAsync(Expression<Func<T, bool>> condition)
        {
            IQueryable<T> queryable = _set.AsQueryable();

            return await queryable.AnyAsync(condition);
        }

        void IBaseRepository<T>.Create(T entity)
        {
            _set.Add(entity);
        }

        void IBaseRepository<T>.Delete(int entityId)
        {
            IQueryable<T> queryable = _set.AsQueryable();

            T entity = queryable.FirstOrDefault(entity => entity.Id == entityId);

            _set.Remove(entity);
        }

        async Task<IEnumerable<T>> IBaseRepository<T>.FindAllAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryable = _set.AsQueryable();

            foreach (Expression<Func<T, object>> include in includes)
            {
                queryable = queryable.Include(include);
            }

            return await queryable.Where(condition).ToListAsync();
        }

        async Task<T> IBaseRepository<T>.FindAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryable = _set.AsQueryable();

            foreach (Expression<Func<T, object>> include in includes)
            {
                queryable = queryable.Include(include);
            }

            return await queryable.Where(condition).FirstOrDefaultAsync();
        }

        async Task<IEnumerable<T>> IBaseRepository<T>.GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryable = _set.AsQueryable();

            foreach (Expression<Func<T, object>> include in includes)
            {
                queryable = queryable.Include(include);
            }

            return await queryable.ToListAsync();
        }

        async Task IBaseRepository<T>.SaveAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }

        async Task<int> IBaseRepository<T>.CountAsync()
        {
            IQueryable<T> queryable = _set.AsQueryable();

            return await queryable.CountAsync();
        }

        async Task<int> IBaseRepository<T>.CountAsync(Expression<Func<T, bool>> condition)
        {
            IQueryable<T> queryable = _set.AsQueryable();

            return await queryable.CountAsync(condition);
        }
    }
}
