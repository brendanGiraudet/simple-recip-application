using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Entities;

public interface IShoppingListItemModel
{
    string UserId { get; set; }
    Guid ProductId { get; set; }
    IProductModel ProductModel { get; set; }
    decimal Quantity { get; set; }
    bool IsDone { get; set; }
}
