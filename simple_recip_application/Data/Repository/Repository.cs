namespace simple_recip_application.Data.Repository;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using simple_recip_application.Data.Persistence.Entities;

public class Repository<T> : IRepository<T> where T : EntityBase
{
    private readonly ApplicationDbContext _dbContext;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

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
        var result = _dbContext.Set<T>().Skip(skip).Take(take);

        if (predicate is not null)
            result = result.Where(predicate);

        return await result.ToListAsync();
    }

    public async Task AddAsync(T? entity)
    {
        if (entity is null) return;

        _dbContext.Set<T>().Add(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T? entity)
    {
        if (entity is null) return;

        _dbContext.Set<T>().Update(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T? entity)
    {
        if (entity is null) return;

        _dbContext.Set<T>().Remove(entity);

        await _dbContext.SaveChangesAsync();
    }
}