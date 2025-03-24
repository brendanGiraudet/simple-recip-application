using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;

namespace simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Factories;

public interface IPlanifiedRecipeModelFactory
{
    IPlanifiedRecipeModel CreatePlanifiedRecipeModel(IRecipeModel? recipe = null, DateTime? planifiedDatetime = null, string? userId = null, string? momentOftheDay = null, Guid? recipeId = null);
}
