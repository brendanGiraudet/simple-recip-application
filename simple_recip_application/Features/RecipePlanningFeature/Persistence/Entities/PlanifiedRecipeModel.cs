using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;

namespace simple_recip_application.Features.RecipePlanningFeature.Persistence.Entities;

public class PlanifiedRecipeModel : IPlanifiedRecipeModel
{
    public Guid RecipeId { get; set; }
    public IRecipeModel RecipeModel
    {
        get => Recipe;
        set => Recipe = (RecipeModel)value;
    }
    public RecipeModel Recipe { get; set; } = default!;
    public DateTime PlanifiedDateTime { get; set; }
    public string UserId { get; set; } = default!;
    public string? MomentOftheDay { get; set; }
}