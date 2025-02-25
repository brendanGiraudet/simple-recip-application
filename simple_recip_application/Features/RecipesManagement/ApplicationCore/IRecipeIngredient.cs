using simple_recip_application.Features.IngredientsManagement.ApplicationCore;

namespace simple_recip_application.Features.RecipesManagement.ApplicationCore;

public interface IRecipeIngredient
{
    public IIngredientModel Ingredient { get; set; }
    public decimal Quantity { get; set; }
}
