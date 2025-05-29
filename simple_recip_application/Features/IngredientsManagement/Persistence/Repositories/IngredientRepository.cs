using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using simple_recip_application.Data;
using simple_recip_application.Data.Persistence.Repositories;
using simple_recip_application.Dtos;
using simple_recip_application.Extensions;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;

namespace simple_recip_application.Features.IngredientsManagement.Persistence.Repositories;

public class IngredientRepository
(
    ApplicationDbContext _dbContext
)
 : EntityBaseRepository<IngredientModel>(_dbContext), IIngredientRepository
{
    public new async Task<MethodResult<IIngredientModel?>> GetByIdAsync(Guid? id)
    {
        var result = await base.GetByIdAsync(id);

        return new MethodResult<IIngredientModel?>(result.Success, result.Item);
    }

    public new async Task<MethodResult<IEnumerable<IIngredientModel>>> GetAsync()
    {
        var result = await base.GetAsync();

        return new MethodResult<IEnumerable<IIngredientModel>>(result.Success, result.Item);
    }

    public async Task<MethodResult<IEnumerable<IIngredientModel>>> GetAsync(int take, int skip, Expression<Func<IIngredientModel, bool>>? predicate, Expression<Func<IIngredientModel, object>>? sort = null)
    {
        try
        {
            if (sort is null)
                sort = c => c.Name;

            var convertedPredicate = predicate?.Convert<IIngredientModel, IngredientModel, bool>();
            var convertedSort = sort?.Convert<IIngredientModel, IngredientModel, object>();

            var ingredientsRequest = base.Get(take, skip, convertedPredicate, convertedSort);

            var ingredients = await ingredientsRequest.Cast<IIngredientModel>().ToListAsync();

            return new MethodResult<IEnumerable<IIngredientModel>>(true, ingredients);
        }
        catch (Exception ex)
        {
            return new MethodResult<IEnumerable<IIngredientModel>>(false, []);
        }
    }

    public async Task<MethodResult> AddAsync(IIngredientModel? entity)
    {
        return await base.AddAsync(entity as IngredientModel);
    }

    public async Task<MethodResult> AddRangeAsync(IEnumerable<IIngredientModel>? entities)
    {
        return await base.AddRangeAsync(entities?.Cast<IngredientModel>() ?? []);
    }

    public async Task<MethodResult> UpdateAsync(IIngredientModel? entity)
    {
        return await base.UpdateAsync(entity as IngredientModel);
    }

    public async Task<MethodResult> DeleteAsync(IIngredientModel? entity)
    {
        return await base.DeleteAsync(entity as IngredientModel);
    }
}
