using simple_recip_application.Data.ApplicationCore.Entities;

namespace simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;

public interface IRecipeModel : IEntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<IRecipeIngredientModel> IngredientModels { get; set; }
    public string Instructions { get; set; }
    public TimeOnly PreparationTime { get; set; }
    public TimeOnly CookingTime { get; set; }
    public byte[] Image { get; set; }
    public string Category { get; set; }
}
