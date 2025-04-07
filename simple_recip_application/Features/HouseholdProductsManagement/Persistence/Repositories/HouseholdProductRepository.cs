using System.Linq.Expressions;
using simple_recip_application.Data;
using simple_recip_application.Data.Persistence.Repositories;
using simple_recip_application.Dtos;
using simple_recip_application.Extensions;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.HouseholdProductsManagement.Persistence.Entities;

namespace simple_recip_application.Features.HouseholdProductsManagement.Persistence.Repositories;

public class HouseholdProductRepository
(
    ApplicationDbContext _dbContext
)
 : EntityBaseRepository<HouseholdProductModel>(_dbContext), IHouseholdProductRepository
{
    public new async Task<MethodResult<IHouseholdProductModel?>> GetByIdAsync(Guid? id)
    {
        var result = await base.GetByIdAsync(id);

        return new MethodResult<IHouseholdProductModel?>(result.Success, result.Item);
    }

    public new async Task<MethodResult<IEnumerable<IHouseholdProductModel>>> GetAsync()
    {
        var result = await base.GetAsync();

        return new MethodResult<IEnumerable<IHouseholdProductModel>>(result.Success, result.Item);
    }

    public async Task<MethodResult<IEnumerable<IHouseholdProductModel>>> GetAsync(int take, int skip, Expression<Func<IHouseholdProductModel, bool>>? predicate)
    {
        var convertedPredicate = predicate?.Convert<IHouseholdProductModel, HouseholdProductModel, bool>();
        
        var result = await base.GetAsync(take, skip, convertedPredicate);

        return new MethodResult<IEnumerable<IHouseholdProductModel>>(result.Success, result.Item.OrderBy(c => c.Name).Cast<IHouseholdProductModel>());
    }

    public async Task<MethodResult> AddAsync(IHouseholdProductModel? entity)
    {
        return await base.AddAsync(entity as HouseholdProductModel);
    }

    public async Task<MethodResult> AddRangeAsync(IEnumerable<IHouseholdProductModel>? entities)
    {
        return await base.AddRangeAsync(entities?.Cast<HouseholdProductModel>() ?? []);
    }

    public async Task<MethodResult> UpdateAsync(IHouseholdProductModel? entity)
    {
        return await base.UpdateAsync(entity as HouseholdProductModel);
    }

    public async Task<MethodResult> DeleteAsync(IHouseholdProductModel? entity)
    {
        return await base.DeleteAsync(entity as HouseholdProductModel);
    }
}
