using simple_recip_application.Features.ProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.UserPantryManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.UserPantryManagement.ApplicationCore.Factories;

public interface IUserPantryItemFactory
{
    IUserPantryItemModel Create(string userId, IProductModel product, decimal quantity);
}
