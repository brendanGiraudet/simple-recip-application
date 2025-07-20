using Fluxor;
using simple_recip_application.Dtos;
using simple_recip_application.Extensions;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Factories;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Repositories;
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
    IState<UserInfosState> _userInfosState,
    ICalendarRepository _calendarRepository
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

            var calendarResult = await _calendarRepository.GetAsync(1, 0, predicate: c => c.CalendarUserAccessModels.Any(a => a.UserId == _userInfosState.Value.UserInfo.Id), sort: c => c.Name);

            if(!calendarResult.Success)
                return new MethodResult<Dictionary<DayOfWeek, List<IPlanifiedRecipeModel>>>(false, []);

            var calendar = calendarResult.Item.FirstOrDefault();

            for (int i = 0; i < 7; i++)
            {
                var dayDate = startOfWeek.AddDays(i);
                var dayOfWeek = dayDate.DayOfWeek;

                var planifiedRecipe = _planifiedRecipeFactory.CreatePlanifiedRecipeModel(
                    recipe: recipesList[i],
                    planifiedDatetime: dayDate.Date,
                    calendarId: calendar.Id,
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
            // Obtenir le nombre total de recettes disponibles
            var totalRecipesResult = await _recipeRepository.CountAsync(c => c.RemoveDate == null);

            if (!totalRecipesResult.Success || totalRecipesResult.Item <= 0)
                return new MethodResult<IPlanifiedRecipeModel>(false, null);

            int totalRecipes = totalRecipesResult.Item;

            // Générer un index aléatoire valide
            var random = new Random();
            int randomIndex = random.Next(0, totalRecipes);

            // Récupérer exactement UNE recette avec pagination
            var recipesResult = await _recipeRepository.GetAsync(take: 1, skip: randomIndex, predicate: c => c.RemoveDate == null);

            if (!recipesResult.Success || !recipesResult.Item.Any())
                return new MethodResult<IPlanifiedRecipeModel>(false, null);

            var recipe = recipesResult.Item.First();

            var newPlanifiedRecipe = _planifiedRecipeFactory.CreatePlanifiedRecipeModel(
                recipe,
                currentPlanifiedRecipe.PlanifiedDateTime,
                currentPlanifiedRecipe.CalendarId,
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