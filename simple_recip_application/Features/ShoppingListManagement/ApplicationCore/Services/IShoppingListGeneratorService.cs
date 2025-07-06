using simple_recip_application.Dtos;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Services;

public interface IShoppingListGeneratorService
{
    Task<MethodResult<string>> GenerateShoppingListCsvContentAsync(IEnumerable<IRecipeModel> recipes);
    
    Task<MethodResult<IEnumerable<IShoppingListItemModel>>> GenerateShoppingListItemsAsync(IEnumerable<IRecipeModel> recipes, string userId);
}
