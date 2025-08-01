using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.RecipesManagement.ApplicationCore.Factories;

public interface IRecipeIngredientFactory
{
    public IRecipeIngredientModel Create(IRecipeModel recipeModel, IIngredientModel ingredientModel, decimal quantity);
}