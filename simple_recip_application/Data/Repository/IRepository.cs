using System.Linq.Expressions;
using simple_recip_application.Data.ApplicationCore;

namespace simple_recip_application.Data.Repository;

public interface IRepository<T> where T : IEntityBase
{
    Task<T?> GetByIdAsync(Guid? id);
    Task<IEnumerable<T>> GetAsync();
    Task<IEnumerable<T>> GetAsync(int take, int skip, Expression<Func<T, bool>>? predicate = null);
    Task AddAsync(T? entity);
    Task DeleteAsync(T? entity);
    Task UpdateAsync(T? entity);
}