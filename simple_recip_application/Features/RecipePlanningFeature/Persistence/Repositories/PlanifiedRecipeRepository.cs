using simple_recip_application.Data;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Data.Persistence.Repositories;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Entities;
using simple_recip_application.Dtos;
using System.Linq.Expressions;
using simple_recip_application.Extensions;
using Microsoft.EntityFrameworkCore;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Repositories;

namespace simple_recip_application.Features.RecipePlanningFeature.Persistence.Repositories;

public class PlanifiedRecipeRepository
(
    ApplicationDbContext _dbContext
)
: Repository<PlanifiedRecipeModel>(_dbContext), IPlanifiedRecipeRepository
{
    public new async Task<MethodResult<IPlanifiedRecipeModel?>> GetByIdAsync(Guid? id)
    {
        var result = await base.GetByIdAsync(id);

        return new MethodResult<IPlanifiedRecipeModel?>(result.Success, result.Item);
    }

    public new async Task<MethodResult<IEnumerable<IPlanifiedRecipeModel>>> GetAsync()
    {
        var result = await base.GetAsync();

        return new MethodResult<IEnumerable<IPlanifiedRecipeModel>>(result.Success, result.Item);
    }

    public async Task<MethodResult<IEnumerable<IPlanifiedRecipeModel>>> GetAsync(int take, int skip, Expression<Func<IPlanifiedRecipeModel, bool>>? predicate, Expression<Func<IPlanifiedRecipeModel, object>>? sort = null)
    {
        try
        {
            if (sort is null)
                sort = c => c.PlanifiedDateTime;

            var convertedPredicate = predicate?.Convert<IPlanifiedRecipeModel, PlanifiedRecipeModel, bool>();
            var convertedSort = sort?.Convert<IPlanifiedRecipeModel, PlanifiedRecipeModel, object>();

            var planifiedRecipes = await base.Get(take, skip, convertedPredicate, convertedSort)
                                             .Include(c => c.Recipe)
                                             .ThenInclude(re => re.Ingredients)
                                             .ThenInclude(c => c.Ingredient)
                                             .ToListAsync();

            return new MethodResult<IEnumerable<IPlanifiedRecipeModel>>(true, planifiedRecipes.Cast<IPlanifiedRecipeModel>());
        }
        catch (System.Exception ex)
        {
            return new MethodResult<IEnumerable<IPlanifiedRecipeModel>>(true, []);
        }
    }

    public async Task<MethodResult> AddAsync(IPlanifiedRecipeModel? entity)
    {
        if (entity.RecipeModel is not null)
            _dbContext.Attach(entity.RecipeModel);

        return await base.AddAsync(entity as PlanifiedRecipeModel);
    }

    public async Task<MethodResult> AddRangeAsync(IEnumerable<IPlanifiedRecipeModel>? entities)
    {
        return await base.AddRangeAsync(entities?.Cast<PlanifiedRecipeModel>() ?? []);
    }

    public async Task<MethodResult> UpdateAsync(IPlanifiedRecipeModel? entity)
    {
        return await base.UpdateAsync(entity as PlanifiedRecipeModel);
    }

    public async Task<MethodResult> DeleteAsync(IPlanifiedRecipeModel? entity)
    {
        return await base.DeleteAsync(entity as PlanifiedRecipeModel);
    }
}
