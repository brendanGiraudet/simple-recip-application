using System.Linq.Expressions;
using simple_recip_application.Dtos;

namespace simple_recip_application.Data.ApplicationCore.Repositories;

public interface IRepository<T> where T : class
{
    Task<MethodResult<T?>> GetByIdAsync(Guid? id);
    Task<MethodResult<IEnumerable<T>>> GetAsync();
    Task<MethodResult<IEnumerable<T>>> GetAsync(int take, int skip, Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? sort = null);
    Task<MethodResult> AddAsync(T? entity);
    Task<MethodResult> AddRangeAsync(IEnumerable<T>? entities);
    Task<MethodResult> DeleteAsync(T? entity);
    Task<MethodResult> UpdateAsync(T? entity);
    Task<MethodResult<int>> CountAsync(Expression<Func<T, bool>>? predicate = null);
}