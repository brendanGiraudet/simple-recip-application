using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.PantryIngredientManagement.ApplicationCore.Factories;

public interface IUserPantryIngredientFactory
{
    IUserPantryIngredientModel Create(string userId, IIngredientModel ingredient, decimal quantity);
}

