using simple_recip_application.Features.TagsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.RecipesManagement.ApplicationCore.Entities;

public interface IRecipeTagModel
{
    public Guid RecipeId { get; set; }
    public IRecipeModel RecipeModel { get; set; }

    public Guid TagId { get; set; }
    public ITagModel TagModel { get; set; }
}
