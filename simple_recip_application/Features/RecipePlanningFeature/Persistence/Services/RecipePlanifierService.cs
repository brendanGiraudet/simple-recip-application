using Fluxor;
using simple_recip_application.Dtos;
using simple_recip_application.Extensions;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Factories;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Services;
using simple_recip_application.Features.RecipePlanningFeature.Enums;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.UserInfos.Store;

namespace simple_recip_application.Features.RecipePlanningFeature.Persistence.Services;

public class RecipePlanifierService
(
    ILogger<RecipePlanifierService> _logger,
    IRecipeRepository _recipeRepository,
    IPlanifiedRecipeModelFactory _planifiedRecipeFactory,
    IState<UserInfosState> _userInfosState
)
 : IRecipePlanifierService
{
    public async Task<MethodResult<Dictionary<DayOfWeek, List<IPlanifiedRecipeModel>>>> GetPlanifiedRecipesForTheWeekAsync(DateTime currentWeekDate)
    {
        try
        {
            Dictionary<DayOfWeek, List<IPlanifiedRecipeModel>> recipesByDay = [];

            var startOfWeek = currentWeekDate.StartOfWeek(DayOfWeek.Monday);

            var recipesResult = await _recipeRepository.GetAsync(7, 0);

            if (!recipesResult.Success || recipesResult.Item.Count() < 7)
                return new MethodResult<Dictionary<DayOfWeek, List<IPlanifiedRecipeModel>>>(false, []);

            var recipesList = recipesResult.Item.ToList();

            for (int i = 0; i < 7; i++)
            {
                var dayDate = startOfWeek.AddDays(i);
                var dayOfWeek = dayDate.DayOfWeek;

                var planifiedRecipe = _planifiedRecipeFactory.CreatePlanifiedRecipeModel(
                    recipe: recipesList[i],
                    planifiedDatetime: dayDate.Date,
                    userId: _userInfosState.Value.UserInfo.Id,
                    momentOftheDay: MomentOfTheDayEnum.Evening.ToString()
                );

                recipesByDay.Add(dayOfWeek, [planifiedRecipe]);
            }

            return new MethodResult<Dictionary<DayOfWeek, List<IPlanifiedRecipeModel>>>(true, recipesByDay);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la planification automatique");

            return new MethodResult<Dictionary<DayOfWeek, List<IPlanifiedRecipeModel>>>(false, []);
        }
    }
    
    public async Task<MethodResult<IPlanifiedRecipeModel>> GetPlanifiedRecipeAutomaticalyAsync(IPlanifiedRecipeModel currentPlanifiedRecipe)
    {
        try
        {
            var recipesResult = await _recipeRepository.GetAsync(1, 0);

            if (!recipesResult.Success || recipesResult.Item.Count() < 1)
                return new MethodResult<IPlanifiedRecipeModel>(false, null);

            var recipe = recipesResult.Item.First();

            var newPlanifiedRecipe = _planifiedRecipeFactory.CreatePlanifiedRecipeModel(
                recipe,
                currentPlanifiedRecipe.PlanifiedDateTime,
                currentPlanifiedRecipe.UserId,
                currentPlanifiedRecipe.MomentOftheDay,
                recipe.Id
            );

            return new MethodResult<IPlanifiedRecipeModel>(true, newPlanifiedRecipe);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la planification automatique");

            return new MethodResult<IPlanifiedRecipeModel>(false, null);
        }
    }
}