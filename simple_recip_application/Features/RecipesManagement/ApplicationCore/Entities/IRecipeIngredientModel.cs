using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.RecipesManagement.ApplicationCore.Entities;

public interface IRecipeIngredientModel
{
    public Guid RecipeId { get; set; }
    public IRecipeModel RecipeModel { get; set; }

    public Guid IngredientId { get; set; }
    public IIngredientModel IngredientModel { get; set; }

    public decimal Quantity { get; set; }
}
