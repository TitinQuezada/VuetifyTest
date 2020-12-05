using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

        void Create(T entity);

        void Delete(int entityId);

        Task<T> FindAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes);
        
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> condition, params Expression<Func<T, object>>[] includes);

        Task<bool> ExistAsync(Expression<Func<T, bool>> condition);

        Task SaveAsync();

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<T, bool>> condition);
    }
}
