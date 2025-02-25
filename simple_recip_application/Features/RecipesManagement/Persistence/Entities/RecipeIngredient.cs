using simple_recip_application.Features.IngredientsManagement.ApplicationCore;
using simple_recip_application.Features.RecipesManagement.ApplicationCore;

namespace simple_recip_application.Features.RecipesManagement.Persistence.Entites;

public class RecipeIngredient : IRecipeIngredient
{
    public IIngredientModel Ingredient { get; set; }
    public decimal Quantity { get; set; }
}