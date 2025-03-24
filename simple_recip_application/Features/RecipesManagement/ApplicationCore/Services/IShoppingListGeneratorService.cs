using simple_recip_application.Dtos;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;

namespace simple_recip_application.Features.RecipesManagement.ApplicationCore.Services;

public interface IShoppingListGeneratorService
{
    Task<MethodResult<string>> GenerateShoppingListCsvContentAsync(IEnumerable<IRecipeModel> recipes);
}
