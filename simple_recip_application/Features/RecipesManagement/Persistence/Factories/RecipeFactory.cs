using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Factories;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;

namespace simple_recip_application.Features.RecipesManagement.Persistence.Factories;

public class RecipeFactory : IRecipeFactory
{
    public IRecipeModel Create(string? name = null, string? description = null, string? instructions = null, TimeOnly? preparationTime = null, TimeOnly? cookingTime = null, byte[]? image = null, string? category = null)
    {
        return new RecipeModel
        {
            Name = name ?? string.Empty,
            Description = description ?? string.Empty,
            Instructions = instructions ?? string.Empty,
            PreparationTime = preparationTime ?? new TimeOnly(),
            CookingTime = cookingTime ?? new TimeOnly(),
            Image = image ?? [],
            Category = category ?? string.Empty,
            IngredientModels = []
        };
    }
}