using simple_recip_application.Features.IngredientsManagement.ApplicationCore;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;

namespace simple_recip_application.Features.RecipesManagement.ApplicationCore.Factories;

public interface IRecipeIngredientFactory
{
    public IRecipeIngredientModel Create(IRecipeModel recipeModel, IIngredientModel ingredientModel, decimal quantity);
}
