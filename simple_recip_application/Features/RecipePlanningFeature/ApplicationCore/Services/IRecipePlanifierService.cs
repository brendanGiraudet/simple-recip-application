using simple_recip_application.Dtos;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;

namespace simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Services;

public interface IRecipePlanifierService
{
    Task<MethodResult<Dictionary<DayOfWeek, List<IPlanifiedRecipeModel>>>> GetPlanifiedRecipesForTheWeekAsync(DateTime currentWeekDate);
}