using simple_recip_application.Data.ApplicationCore.Repository;
using simple_recip_application.Data.Persistence.Entities;
using simple_recip_application.Dtos;

namespace simple_recip_application.Data.Persistence.Repository;

public class EntityBaseRepository<T>
(
    ApplicationDbContext _dbContext
)
: Repository<T>(_dbContext), IRepository<T> where T : EntityBase
{
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