using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;

namespace simple_recip_application.Features.RecipesManagement.ApplicationCore.Services;

public interface IShoppingListGenerator
{
    Task<string> GenerateShoppingListCsvContentAsync(IEnumerable<IRecipeModel> recipes);
}
