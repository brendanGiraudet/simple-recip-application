using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.ShoppingListManagement.Persistence.Entities;

public class ShoppingListItemModel : IShoppingListItemModel
{
    public string UserId { get; set; } = default!;

    public Guid ProductId { get; set; }

    public IProductModel ProductModel { get; set; } = default!;

    public decimal Quantity { get; set; }

    public bool IsDone { get; set; }
}

