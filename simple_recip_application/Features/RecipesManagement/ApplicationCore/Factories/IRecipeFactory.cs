using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.RecipesManagement.ApplicationCore.Factories;

public interface IRecipeFactory
{
    public IRecipeModel Create(string? name = null, string? description = null, string? instructions = null, TimeOnly? preparationTime = null, TimeOnly? cookingTime = null, byte[]? image = null, string? category = null);
}
