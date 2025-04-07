using simple_recip_application.Features.HouseholdProductsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.UserPantryManagement.ApplicationCore.Entities;

public interface IUserPantryItemModel
{
    string UserId { get; set; }
    Guid ProductId { get; set; }
    IProductModel ProductModel { get; set; }
    decimal Quantity { get; set; }
}
