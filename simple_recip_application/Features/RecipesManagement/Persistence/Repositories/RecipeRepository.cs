using System.Linq.Expressions;
using simple_recip_application.Data;
using simple_recip_application.Data.Persistence.Repository;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;
using simple_recip_application.Extensions;
using Microsoft.EntityFrameworkCore;

namespace simple_recip_application.Features.RecipesManagement.Persistence.Repositories;

public class RecipeRepository
(
    ApplicationDbContext _dbContext
)
: Repository<RecipeModel>(_dbContext), IRecipeRepository
{
    public new async Task<IRecipeModel?> GetByIdAsync(Guid? id)
    {
        var ingredient = await _dbContext.Set<RecipeModel>()
                                         .Include(c => c.Ingredients)
                                         .ThenInclude(c => c.Ingredient)
                                         .FirstOrDefaultAsync(c => c.Id == id);
        return ingredient;
    }

    public new async Task<IEnumerable<IRecipeModel>> GetAsync()
    {
        var ingredients = await base.GetAsync();

        return ingredients.Cast<IRecipeModel>().ToList();
    }

    public async Task<IEnumerable<IRecipeModel>> GetAsync(int take, int skip, Expression<Func<IRecipeModel, bool>>? predicate)
    {
        var convertedPredicate = predicate?.Convert<IRecipeModel, RecipeModel, bool>();

        var ingredients = base.Get(take, skip, convertedPredicate);

        return await ingredients.OrderBy(c => c.Name).Cast<IRecipeModel>().ToListAsync();
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
