using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Entities;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Factories;
using simple_recip_application.Features.ShoppingListManagement.Persistence.Entities;

namespace simple_recip_application.Features.ShoppingListManagement.Persistence.Factories;

public class ShoppingListItemModelFactory : IShoppingListItemModelFactory
{
    public IShoppingListItemModel Create(string userId, IProductModel product, decimal quantity, bool isDone) =>
        new ShoppingListItemModel
        {
            UserId = userId,
            ProductId = product.Id ?? Guid.NewGuid(),
            ProductModel = product,
            Quantity = quantity,
            IsDone = isDone
        };
}
