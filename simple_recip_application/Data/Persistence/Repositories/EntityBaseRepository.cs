using System.Linq.Expressions;
using simple_recip_application.Data.ApplicationCore.Repositories;
using simple_recip_application.Data.Persistence.Entities;
using simple_recip_application.Dtos;

namespace simple_recip_application.Data.Persistence.Repositories;

public class EntityBaseRepository<T>
(
    ApplicationDbContext _dbContext
)
: Repository<T>(_dbContext), IRepository<T> where T : EntityBase
{
    protected override IQueryable<T> Get(int take, int skip, Expression<Func<T, bool>>? predicate = null)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (predicate is not null)
            query = query.Where(predicate);

        return query.OrderBy(c => c.Id).Skip(skip).Take(take);
    }

    public override async Task<MethodResult> AddAsync(T? entity)
    {
        if (entity is null) return new MethodResult(false);

        try
        {
            entity.CreationDate = DateTime.UtcNow;

            _dbContext.Set<T>().Add(entity);

            await _dbContext.SaveChangesAsync();

            return new MethodResult(true);
        }
        catch (System.Exception ex)
        {
            return new MethodResult(false);
        }
    }

    public override async Task<MethodResult> UpdateAsync(T? entity)
    {
        if (entity is null) return new MethodResult(false);

        try
        {
            entity.ModificationDate = DateTime.UtcNow;

            _dbContext.Set<T>().Update(entity);

            await _dbContext.SaveChangesAsync();

            return new MethodResult(true);
        }
        catch (System.Exception ex)
        {
            return new MethodResult(false);
        }
    }

    public override async Task<MethodResult> DeleteAsync(T? entity)
    {
        if (entity is null) return new MethodResult(false);

        entity.ModificationDate = DateTime.UtcNow;

        entity.RemoveDate = DateTime.UtcNow;

        return await UpdateAsync(entity);
    }
}