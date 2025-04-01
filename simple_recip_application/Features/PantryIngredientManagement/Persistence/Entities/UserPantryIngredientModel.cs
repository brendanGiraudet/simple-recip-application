
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;
using simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.PantryIngredientManagement.Persistence.Entities;

public class UserPantryIngredientModel : IUserPantryIngredientModel
{
    public string UserId { get; set; } = default!;

    public Guid IngredientId { get; set; }

    public IngredientModel Ingredient { get; set; } = default!;
    public IIngredientModel IngredientModel
    {
        get => Ingredient;
        set => Ingredient = (IngredientModel)value;
    }

    public decimal Quantity { get; set; }
}
