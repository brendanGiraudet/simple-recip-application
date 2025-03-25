using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using simple_recip_application.Data.ApplicationCore.Repositories;
using simple_recip_application.Dtos;

namespace simple_recip_application.Data.Persistence.Repositories;

public class Repository<T>
(
    ApplicationDbContext _dbContext
)
: IRepository<T> where T : class
{
    public virtual async Task<MethodResult<T?>> GetByIdAsync(Guid? id)
    {
        if (id is null) return new MethodResult<T?>(false, null);

        try
        {
            var item = await _dbContext.Set<T>().FindAsync(new object[] { id });

            return new MethodResult<T?>(true, item);
        }
        catch (System.Exception ex)
        {
            return new MethodResult<T?>(false, null);
        }
    }

    public virtual async Task<MethodResult<IEnumerable<T>>> GetAsync()
    {
        try
        {
            var items = await _dbContext.Set<T>().ToListAsync();

            return new MethodResult<IEnumerable<T>>(true, items);
        }
        catch (System.Exception ex)
        {
            return new MethodResult<IEnumerable<T>>(false, []);
        }
    }

    public virtual async Task<MethodResult<IEnumerable<T>>> GetAsync(int take, int skip, Expression<Func<T, bool>>? predicate = null)
    {
        try
        {
            var items = await Get(take, skip, predicate).ToListAsync();

            return new MethodResult<IEnumerable<T>>(true, items);
        }
        catch (System.Exception ex)
        {

            return new MethodResult<IEnumerable<T>>(false, []);
        }
    }

    protected virtual IQueryable<T> Get(int take, int skip, Expression<Func<T, bool>>? predicate = null)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (predicate is not null)
            query = query.Where(predicate);

        return query.Skip(skip).Take(take);
    }

    public virtual async Task<MethodResult> AddAsync(T? entity)
    {
        if (entity is null) return new MethodResult(false);

        try
        {
            _dbContext.Set<T>().Add(entity);

            await _dbContext.SaveChangesAsync();

            return new MethodResult(true);
        }
        catch (System.Exception ex)
        {
            return new MethodResult(false);
        }
    }

    public virtual async Task<MethodResult> UpdateAsync(T? entity)
    {
        if (entity is null) return new MethodResult(false);

        try
        {
            _dbContext.Set<T>().Update(entity);

            await _dbContext.SaveChangesAsync();

            return new MethodResult(true);
        }
        catch (System.Exception ex)
        {
            return new MethodResult(false);
        }
    }

    public virtual async Task<MethodResult> DeleteAsync(T? entity)
    {
        if (entity is null) return new MethodResult(false);

        try
        {
            _dbContext.Set<T>().Remove(entity);

            await _dbContext.SaveChangesAsync();

            return new MethodResult(true);
        }
        catch (System.Exception ex)
        {
            return new MethodResult(false);
        }
    }
}