using CustomerDbManager.Domain.Entities.Common;
using RabbitListener.Application.Interfaces.Repositories.Common;
using System.Linq.Expressions;

namespace CustomerDbManager.Application.Interfaces.Repositories.Common
{
    public interface IGenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAll();
        Task<List<T>> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> order = null, params Expression<Func<T, object>>[] includes);
        Task<List<T>> Get(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task<T> AddAsync(T entity);
        T Update(T entity);
    }
}
