using Microsoft.EntityFrameworkCore;
using simple_recip_application.Data;
using simple_recip_application.Data.Persistence.Repositories;
using simple_recip_application.Dtos;
using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Entities;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.ShoppingListManagement.Persistence.Entities;

namespace simple_recip_application.Features.ShoppingListManagement.Persistence.Repositories;

public class ShoppingListRepository
(
    ApplicationDbContext _dbContext,
    IIngredientRepository _ingredientRepository,
    IHouseholdProductRepository _householdProductRepository
)
    : Repository<ShoppingListItemModel>(_dbContext), IShoppingListRepository
{
    public async Task<MethodResult<IEnumerable<IShoppingListItemModel>>> GetShoppingListItemsAsync(string userId)
    {
        try
        {
            var items = await _dbContext.Set<ShoppingListItemModel>()
            .Where(pi => pi.UserId == userId)
            .ToListAsync();

            foreach (var product in items)
            {
                var ingredientModelExistResult = await _ingredientRepository.GetByIdAsync(product.ProductId);

                if (ingredientModelExistResult.Success && ingredientModelExistResult.Item is not null)
                    product.ProductModel = ingredientModelExistResult.Item;

                else if (ingredientModelExistResult.Success && ingredientModelExistResult.Item is null)
                {
                    var householdModelExistResult = await _householdProductRepository.GetByIdAsync(product.ProductId);

                    if (householdModelExistResult.Success && householdModelExistResult.Item is not null)
                        product.ProductModel = householdModelExistResult.Item;
                }
            }

            return new MethodResult<IEnumerable<IShoppingListItemModel>>(true, items);
        }
        catch (Exception ex)
        {
            return new MethodResult<IEnumerable<IShoppingListItemModel>>(false, []);
        }
    }

    public async Task<MethodResult<IShoppingListItemModel?>> GetShoppingListItemAsync(string userId, Guid productId)
    {
        try
        {
            var item = await _dbContext.Set<ShoppingListItemModel>()
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(pi => pi.UserId == userId && 
                                                                  pi.ProductId == productId);

            return new MethodResult<IShoppingListItemModel?>(true, item);
        }
        catch (Exception ex)
        {
            return new MethodResult<IShoppingListItemModel?>(true, null);
        }
    }

    public async Task<MethodResult> AddAsync(IShoppingListItemModel shoppingListItem)
    {
        if (shoppingListItem.ProductModel is not null)
            _dbContext.Attach(shoppingListItem.ProductModel);

        return await base.AddAsync(shoppingListItem as ShoppingListItemModel);
    }
    
    public async Task<MethodResult> AddRangeAsync(IEnumerable<IShoppingListItemModel> shoppingListItems)
    {
        foreach (var shoppingListItem in shoppingListItems)
        {
            if (shoppingListItem.ProductModel is not null)
                _dbContext.Attach(shoppingListItem.ProductModel);
        }

        return await base.AddRangeAsync(shoppingListItems.Select(c => c as ShoppingListItemModel));
    }

    public async Task<MethodResult> UpdateAsync(IShoppingListItemModel shoppingListItem)
    {
        return await base.UpdateAsync(shoppingListItem as ShoppingListItemModel);
    }

    public async Task<MethodResult> DeleteAsync(IShoppingListItemModel shoppingListItem)
    {
        return await base.DeleteAsync(shoppingListItem as ShoppingListItemModel);
    }
    
    public async Task<MethodResult> DeleteRangeAsync(IEnumerable<IShoppingListItemModel> shoppingListItems)
    {
        return await base.DeleteRangeAsync(shoppingListItems.Select(c => c as ShoppingListItemModel));
    }
}

