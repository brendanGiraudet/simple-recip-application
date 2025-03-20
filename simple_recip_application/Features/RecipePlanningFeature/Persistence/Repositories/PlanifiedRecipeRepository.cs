using simple_recip_application.Data;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Data.Persistence.Repository;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Entities;
using simple_recip_application.Dtos;
using System.Linq.Expressions;
using simple_recip_application.Extensions;

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

    public async Task<MethodResult<IEnumerable<IPlanifiedRecipeModel>>> GetAsync(int take, int skip, Expression<Func<IPlanifiedRecipeModel, bool>>? predicate)
    {
        var convertedPredicate = predicate?.Convert<IPlanifiedRecipeModel, PlanifiedRecipeModel, bool>();
        
        var result = await base.GetAsync(take, skip, convertedPredicate);

        return new MethodResult<IEnumerable<IPlanifiedRecipeModel>>(result.Success, result.Item.OrderBy(c => c.PlanifiedDateTime).Cast<IPlanifiedRecipeModel>());
    }

    public async Task<MethodResult> AddAsync(IPlanifiedRecipeModel? entity)
    {
        return await base.AddAsync(entity as PlanifiedRecipeModel);
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
