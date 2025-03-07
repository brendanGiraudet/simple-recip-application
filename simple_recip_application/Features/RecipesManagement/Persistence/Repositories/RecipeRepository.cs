using System.Linq.Expressions;
using simple_recip_application.Data;
using simple_recip_application.Data.Persistence.Repository;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;
using simple_recip_application.Extensions;

namespace simple_recip_application.Features.RecipesManagement.Persistence.Repositories;

public class RecipeRepository
(
    ApplicationDbContext _dbContext
)
: Repository<RecipeModel>(_dbContext), IRecipeRepository
{
    public new async Task<IRecipeModel?> GetByIdAsync(Guid? id)
    {
        return await base.GetByIdAsync(id);
    }

    public new async Task<IEnumerable<IRecipeModel>> GetAsync()
    {
        var ingredients = await base.GetAsync();

        return ingredients.Cast<IRecipeModel>().ToList();
    }

    public async Task<IEnumerable<IRecipeModel>> GetAsync(int take, int skip, Expression<Func<IRecipeModel, bool>>? predicate, Expression<Func<IRecipeModel, object>>? include)
    {
        var convertedPredicate = predicate?.Convert<IRecipeModel, RecipeModel, bool>();
        
        var ingredients = await base.GetAsync(take, skip, convertedPredicate, i => i.Ingredients);

        return ingredients.Cast<IRecipeModel>();
    }

    public async Task AddAsync(IRecipeModel? entity)
    {
        await base.AddAsync(entity as RecipeModel);
    }

    public async Task UpdateAsync(IRecipeModel? entity)
    {
        await base.UpdateAsync(entity as RecipeModel);
    }

    public async Task DeleteAsync(IRecipeModel? entity)
    {
        await base.DeleteAsync(entity as RecipeModel);
    }
}
