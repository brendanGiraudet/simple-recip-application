using simple_recip_application.Dtos;
using simple_recip_application.Features.UserPantryManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.UserPantryManagement.ApplicationCore.Repositories;

public interface IUserPantryItemRepository
{
    Task<MethodResult<IEnumerable<IUserPantryItemModel>>> GetUserPantryItemsAsync(string userId);
    Task<MethodResult<IUserPantryItemModel?>> GetUserPantryItemAsync(string userId, Guid productId);
    Task<MethodResult> AddAsync(IUserPantryItemModel pantryItem);
    Task<MethodResult> UpdateAsync(IUserPantryItemModel pantryItem);
    Task<MethodResult> DeleteAsync(IUserPantryItemModel pantryItem);
}
