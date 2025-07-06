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

    public async Task<MethodResult> AddAsync(IShoppingListItemModel pantryItem)
    {
        if (pantryItem.ProductModel is not null)
            _dbContext.Attach(pantryItem.ProductModel);

        return await base.AddAsync(pantryItem as ShoppingListItemModel);
    }

    public async Task<MethodResult> UpdateAsync(IShoppingListItemModel pantryItem)
    {
        return await base.UpdateAsync(pantryItem as ShoppingListItemModel);
    }

    public async Task<MethodResult> DeleteAsync(IShoppingListItemModel pantryItem)
    {
        return await base.DeleteAsync(pantryItem as ShoppingListItemModel);
    }
}

