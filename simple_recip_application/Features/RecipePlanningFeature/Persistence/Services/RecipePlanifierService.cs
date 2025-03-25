using simple_recip_application.Dtos;
using simple_recip_application.Extensions;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Factories;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Services;
using simple_recip_application.Features.RecipePlanningFeature.Enums;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Repositories;

namespace simple_recip_application.Features.RecipePlanningFeature.Persistence.Services;

public class RecipePlanifierService
(
    ILogger<RecipePlanifierService> _logger,
    IRecipeRepository _recipeRepository,
    IPlanifiedRecipeModelFactory _planifiedRecipeFactory
)
 : IRecipePlanifierService
{
    public async Task<MethodResult<Dictionary<DayOfWeek, List<IPlanifiedRecipeModel>>>> GetPlanifiedRecipesForTheWeekAsync(DateTime currentWeekDate)
    {
        try
        {
            string userId = "currentUserId";
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
                    userId: userId,
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
}