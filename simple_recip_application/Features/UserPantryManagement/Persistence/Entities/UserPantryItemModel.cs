using simple_recip_application.Features.ProductsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.UserPantryManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.UserPantryManagement.Persistence.Entities;

public class UserPantryItemModel : IUserPantryItemModel
{
    public string UserId { get; set; } = default!;

    public Guid ProductId { get; set; }

    public IProductModel ProductModel { get; set; } = default!;

    public decimal Quantity { get; set; }
}

