using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;

namespace simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;

public interface IPlanifiedRecipeModel
{
    public IRecipeModel RecipeModel { get; set; }
    public DateTime PlanifiedDateTime { get; set; }
    public string UserId { get; set; }
    public string? MomentOftheDay { get; set; }
}