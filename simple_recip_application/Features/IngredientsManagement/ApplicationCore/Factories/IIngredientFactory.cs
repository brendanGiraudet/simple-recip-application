using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.IngredientsManagement.ApplicationCore.Factories;

public interface IIngredientFactory
{
    IIngredientModel CreateIngredient(string? name = null, byte[]? image = null);
}
