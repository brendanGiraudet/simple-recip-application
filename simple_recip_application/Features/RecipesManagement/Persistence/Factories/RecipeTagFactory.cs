using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Factories;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.RecipesManagement.Persistence.Factories;

public class RecipeTagFactory : IRecipeTagFactory
{
    public IRecipeTagModel Create(IRecipeModel recipeModel, ITagModel tagModel)
    {
        return new RecipeTagModel
        {
            RecipeModel = recipeModel,
            TagModel = tagModel
        };
    }
}
