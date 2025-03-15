using System.Linq.Expressions;
using simple_recip_application.Data.ApplicationCore.Entities;
using simple_recip_application.Dtos;

namespace simple_recip_application.Data.ApplicationCore.Repository;

public interface IRepository<T> where T : IEntityBase
{
    Task<MethodResult<T?>> GetByIdAsync(Guid? id);
    Task<MethodResult<IEnumerable<T>>> GetAsync();
    Task<MethodResult<IEnumerable<T>>> GetAsync(int take, int skip, Expression<Func<T, bool>>? predicate = null);
    Task<MethodResult> AddAsync(T? entity);
    Task<MethodResult> DeleteAsync(T? entity);
    Task<MethodResult> UpdateAsync(T? entity);
}