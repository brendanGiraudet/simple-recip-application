using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Factories;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;

namespace simple_recip_application.Features.RecipePlanningFeature.Persistence.Factories;

public class PlanifiedRecipeModelFactory : IPlanifiedRecipeModelFactory
{
    public IPlanifiedRecipeModel CreatePlanifiedRecipeModel(IRecipeModel? recipe = null, DateTime? planifiedDatetime = null, Guid? calendarId = null, string? momentOftheDay = null, Guid? recipeId = null) => new PlanifiedRecipeModel
    {
        MomentOftheDay = momentOftheDay,
        PlanifiedDateTime = planifiedDatetime ?? DateTime.UtcNow,
        RecipeModel = recipe,
        CalendarId = calendarId ?? Guid.Empty,
        RecipeId = recipeId ?? new ()
    };
}
