using System.Linq.Expressions;
using simple_recip_application.Data;
using simple_recip_application.Data.Persistence.Repository;
using simple_recip_application.Extensions;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;

namespace simple_recip_application.Features.IngredientsManagement.Persistence.Repositories;

public class IngredientRepository
(
    ApplicationDbContext _dbContext
)
 : Repository<IngredientModel>(_dbContext), IIngredientRepository
{
    public new async Task<IIngredientModel?> GetByIdAsync(Guid? id)
    {
        return await base.GetByIdAsync(id);
    }

    public new async Task<IEnumerable<IIngredientModel>> GetAsync()
    {
        var ingredients = await base.GetAsync();
        return ingredients.Cast<IIngredientModel>().ToList();
    }

    public async Task<IEnumerable<IIngredientModel>> GetAsync(int take, int skip, Expression<Func<IIngredientModel, bool>>? predicate)
    {
        var convertedPredicate = predicate?.Convert<IIngredientModel, IngredientModel, bool>();
        
        var ingredients = await base.GetAsync(take, skip, convertedPredicate);

        return ingredients.Cast<IIngredientModel>();
    }

    public async Task AddAsync(IIngredientModel? entity)
    {
        await base.AddAsync(entity as IngredientModel);
    }

    public async Task UpdateAsync(IIngredientModel? entity)
    {
        await base.UpdateAsync(entity as IngredientModel);
    }

    public async Task DeleteAsync(IIngredientModel? entity)
    {
        await base.DeleteAsync(entity as IngredientModel);
    }
}
