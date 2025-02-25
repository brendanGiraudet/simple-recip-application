using simple_recip_application.Data.Persistence.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore;

namespace simple_recip_application.Features.RecipesManagement.Persistence.Entites;

public class RecipeModel : EntityBase, IRecipeModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<IRecipeIngredient> Ingredients { get; set; }
    public string Instructions { get; set; }
    public TimeSpan PreparationTime { get; set; }
    public TimeSpan CookingTime { get; set; }
    public string? ImageUrl { get; set; }
    public string Category { get; set; }
}
