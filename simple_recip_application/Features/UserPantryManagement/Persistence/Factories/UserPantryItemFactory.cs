using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.UserPantryManagement.ApplicationCore.Entities;
using simple_recip_application.Features.UserPantryManagement.ApplicationCore.Factories;
using simple_recip_application.Features.UserPantryManagement.Persistence.Entities;

namespace simple_recip_application.Features.UserPantryManagement.Persistence.Factories;

public class UserPantryItemFactory : IUserPantryItemFactory
{
    public IUserPantryItemModel Create(string userId, IProductModel product, decimal quantity) =>
        new UserPantryItemModel
        {
            UserId = userId,
            ProductId = product.Id ?? Guid.NewGuid(),
            ProductModel = product,
            Quantity = quantity
        };
}
