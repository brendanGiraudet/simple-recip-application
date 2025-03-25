using System.Linq.Expressions;
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

    public async Task<MethodResult<IEnumerable<IIngredientModel>>> GetAsync(int take, int skip, Expression<Func<IIngredientModel, bool>>? predicate)
    {
        var convertedPredicate = predicate?.Convert<IIngredientModel, IngredientModel, bool>();
        
        var result = await base.GetAsync(take, skip, convertedPredicate);

        return new MethodResult<IEnumerable<IIngredientModel>>(result.Success, result.Item.OrderBy(c => c.Name).Cast<IIngredientModel>());
    }

    public async Task<MethodResult> AddAsync(IIngredientModel? entity)
    {
        return await base.AddAsync(entity as IngredientModel);
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
