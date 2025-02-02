using System.Linq.Expressions;
using simple_recip_application.Data.ApplicationCore;

namespace simple_recip_application.Data.Repository;

interface IRepository<T> where T : EntityBase
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAsync();
    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task UpdateAsync(T entity);
}