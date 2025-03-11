using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using simple_recip_application.Data.ApplicationCore.Repository;
using simple_recip_application.Data.Persistence.Entities;

namespace simple_recip_application.Data.Persistence.Repository;

public class Repository<T> 
(
    ApplicationDbContext _dbContext
)
: IRepository<T> where T : EntityBase
{
    public virtual async Task<T?> GetByIdAsync(Guid? id)
    {
        if (id is null) return null;

        return await _dbContext.Set<T>().FindAsync(new object[] { id });
    }

    public virtual async Task<IEnumerable<T>> GetAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> GetAsync(int take, int skip, Expression<Func<T, bool>>? predicate = null)
    {
        return await Get(take, skip, predicate).ToListAsync();
    }
    
    protected virtual IQueryable<T> Get(int take, int skip, Expression<Func<T, bool>>? predicate = null)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (predicate is not null)
            query = query.Where(predicate);

        return query.Skip(skip).Take(take);
    }

    public async Task AddAsync(T? entity)
    {
        if (entity is null) return;

        entity.CreationDate = DateTime.UtcNow;

        _dbContext.Set<T>().Add(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T? entity)
    {
        if (entity is null) return;

        entity.ModificationDate = DateTime.UtcNow;

        _dbContext.Set<T>().Update(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T? entity)
    {
        if (entity is null) return;

        entity.RemoveDate = DateTime.UtcNow;

        await UpdateAsync(entity);
    }
}