using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entities;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.TagsManagement.Persistence.Entities;

namespace simple_recip_application.Features.RecipesManagement.Persistence.Entites;

public class RecipeTagModel : IRecipeTagModel
{
    public Guid RecipeId { get; set; }
    public RecipeModel Recipe { get; set; } = default!;
    public IRecipeModel RecipeModel
    {
        get => Recipe;
        set => Recipe = (RecipeModel)value;
    }

    public Guid TagId { get; set; }
    public TagModel Tag { get; set; } = default!;
    public ITagModel TagModel
    {
        get => Tag;
        set => Tag = (TagModel)value;
    }
}