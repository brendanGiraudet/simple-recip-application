using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Factories;

public interface IShoppingListItemModelFactory
{
    IShoppingListItemModel Create(string userId, IProductModel product, decimal quantity, bool isDone);
}
