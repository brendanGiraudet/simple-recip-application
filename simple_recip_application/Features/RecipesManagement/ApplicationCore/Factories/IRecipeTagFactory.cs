using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entities;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.RecipesManagement.ApplicationCore.Factories;

public interface IRecipeTagFactory
{
    public IRecipeTagModel Create(IRecipeModel recipeModel, ITagModel tagModel);
}