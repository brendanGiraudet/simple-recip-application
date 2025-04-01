using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.Entities;

public interface IUserPantryIngredientModel
{
    string UserId { get; set; }
    Guid IngredientId { get; set; }
    IIngredientModel IngredientModel { get; set; }
    decimal Quantity { get; set; }
}