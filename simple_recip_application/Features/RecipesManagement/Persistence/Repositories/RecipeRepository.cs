using System.Linq.Expressions;
using simple_recip_application.Data;
using simple_recip_application.Data.Persistence.Repositories;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;
using simple_recip_application.Extensions;
using Microsoft.EntityFrameworkCore;
using simple_recip_application.Dtos;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Repositories;

namespace simple_recip_application.Features.RecipesManagement.Persistence.Repositories;

public class RecipeRepository
(
    ApplicationDbContext _dbContext
)
: EntityBaseRepository<RecipeModel>(_dbContext), IRecipeRepository
{
    public new async Task<MethodResult<IRecipeModel?>> GetByIdAsync(Guid? id)
    {
        try
        {
            var recipe = await _dbContext.Set<RecipeModel>()
                                         .Include(c => c.Ingredients)
                                         .ThenInclude(c => c.Ingredient)
                                         .Include(c=> c.Tags)
                                         .ThenInclude(c => c.Tag)
                                         .FirstOrDefaultAsync(c => c.Id == id);

            return new MethodResult<IRecipeModel?>(true, recipe);
        }
        catch (System.Exception ex)
        {
            return new MethodResult<IRecipeModel?>(false, null);
        }
    }

    public new async Task<MethodResult<IEnumerable<IRecipeModel>>> GetAsync()
    {
        var result = await base.GetAsync();

        return new MethodResult<IEnumerable<IRecipeModel>>(result.Success, result.Item);
    }

    public async Task<MethodResult<IEnumerable<IRecipeModel>>> GetAsync(int take, int skip, Expression<Func<IRecipeModel, bool>>? predicate = null, Expression<Func<IRecipeModel, object>>? sort = null)
    {
        try
        {
            if(sort is null)
                sort = c => c.Name;

            var convertedPredicate = predicate?.Convert<IRecipeModel, RecipeModel, bool>();
            var convertedSort = sort?.Convert<IRecipeModel, RecipeModel, object>();

            var recipesRequest = base.Get(take, skip, convertedPredicate, convertedSort)
                                     .Select(c => new RecipeModel
                                     {
                                         Id = c.Id,
                                         Name = c.Name,
                                         Image = c.Image,
                                         CookingTime = c.CookingTime,
                                         PreparationTime = c.PreparationTime,
                                     });    

            var recipes = await recipesRequest.Cast<IRecipeModel>().ToListAsync();

            return new MethodResult<IEnumerable<IRecipeModel>>(true, recipes);
        }
        catch (System.Exception ex)
        {
            return new MethodResult<IEnumerable<IRecipeModel>>(false, []);
        }
    }

    public async Task<MethodResult> AddAsync(IRecipeModel? entity)
    {
        return await base.AddAsync(entity as RecipeModel);
    }
    
    public async Task<MethodResult> AddRangeAsync(IEnumerable<IRecipeModel>? entities)
    {
        return await base.AddRangeAsync(entities?.Cast<RecipeModel>() ?? []);
    }

    public async Task<MethodResult> UpdateAsync(IRecipeModel? entity)
    {
        return await base.UpdateAsync(entity as RecipeModel);
    }

    public async Task<MethodResult> DeleteAsync(IRecipeModel? entity)
    {
        return await base.DeleteAsync(entity as RecipeModel);
    }
}
