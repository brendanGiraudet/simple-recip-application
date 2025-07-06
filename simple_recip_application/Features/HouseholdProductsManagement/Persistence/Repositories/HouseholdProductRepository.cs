using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using simple_recip_application.Data;
using simple_recip_application.Data.Persistence.Repositories;
using simple_recip_application.Dtos;
using simple_recip_application.Extensions;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.HouseholdProductsManagement.Persistence.Entities;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;

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

    public async Task<MethodResult<IEnumerable<IHouseholdProductModel>>> GetAsync(int take, int skip, Expression<Func<IHouseholdProductModel, bool>>? predicate, Expression<Func<IHouseholdProductModel, object>>? sort = null)
    {
        try
        {
            if (sort is null)
                sort = c => c.Name;

            var convertedPredicate = predicate?.Convert<IHouseholdProductModel, HouseholdProductModel, bool>();
            var convertedSort = sort?.Convert<IHouseholdProductModel, HouseholdProductModel, object>();

            var householdProductsRequest = base.Get(take, skip, convertedPredicate, convertedSort);

            var householdProducts = await householdProductsRequest.Cast<IHouseholdProductModel>().ToListAsync();

            return new MethodResult<IEnumerable<IHouseholdProductModel>>(true, householdProducts);
        }
        catch (Exception ex)
        {
            return new MethodResult<IEnumerable<IHouseholdProductModel>>(false, []);
        }
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

    public async Task<MethodResult> UpdateRangeAsync(IEnumerable<IHouseholdProductModel>? entities)
    {
        return await base.UpdateRangeAsync(entities as IEnumerable<HouseholdProductModel>);
    }
    public async Task<MethodResult> DeleteAsync(IHouseholdProductModel? entity)
    {
        return await base.DeleteAsync(entity as HouseholdProductModel);
    }

    public async Task<MethodResult> DeleteRangeAsync(IEnumerable<IHouseholdProductModel>? entities)
    {
        return await base.DeleteRangeAsync(entities as IEnumerable<HouseholdProductModel>);
    }

    public async Task<MethodResult<int>> CountAsync(Expression<Func<IHouseholdProductModel, bool>>? predicate = null)
    {
        var convertedPredicate = predicate?.Convert<IHouseholdProductModel, HouseholdProductModel, bool>();

        return await base.CountAsync(convertedPredicate);
    }
}
