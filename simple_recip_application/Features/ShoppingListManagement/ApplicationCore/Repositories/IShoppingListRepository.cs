using simple_recip_application.Dtos;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Repositories;

public interface IShoppingListRepository
{
    Task<MethodResult<IEnumerable<IShoppingListItemModel>>> GetShoppingListItemsAsync(string userId);
    Task<MethodResult<IShoppingListItemModel?>> GetShoppingListItemAsync(string userId, Guid productId);
    Task<MethodResult> AddAsync(IShoppingListItemModel pantryItem);
    Task<MethodResult> UpdateAsync(IShoppingListItemModel pantryItem);
    Task<MethodResult> DeleteAsync(IShoppingListItemModel pantryItem);
    Task<MethodResult> AddRangeAsync(IEnumerable<IShoppingListItemModel> shoppingListItems);
    Task<MethodResult> DeleteRangeAsync(IEnumerable<IShoppingListItemModel> shoppingListItems);
}
