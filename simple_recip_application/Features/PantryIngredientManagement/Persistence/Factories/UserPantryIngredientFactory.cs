using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.Entities;
using simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.Factories;
using simple_recip_application.Features.PantryIngredientManagement.Persistence.Entities;

namespace simple_recip_application.Features.PantryIngredientManagement.Persistence.Factories;

public class UserPantryIngredientFactory : IUserPantryIngredientFactory
{
    public IUserPantryIngredientModel Create(string userId, IIngredientModel ingredient, decimal quantity) =>
        new UserPantryIngredientModel
        {
            UserId = userId,
            IngredientId = ingredient.Id ?? Guid.NewGuid(),
            IngredientModel = ingredient,
            Quantity = quantity
        };
}
