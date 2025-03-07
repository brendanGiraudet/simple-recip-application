using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Factories;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;

namespace simple_recip_application.Features.RecipesManagement.Persistence.Factories;

public class RecipeIngredientFactory : IRecipeIngredientFactory
{
    public IRecipeIngredientModel Create(IRecipeModel recipeModel, IIngredientModel ingredientModel, decimal quantity)
    {
        return new RecipeIngredientModel
        {
            RecipeModel = recipeModel,
            IngredientModel = ingredientModel,
            Quantity = quantity
        };
    }
}
