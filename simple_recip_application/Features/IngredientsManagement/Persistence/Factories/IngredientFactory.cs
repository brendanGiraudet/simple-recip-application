namespace simple_recip_application.Features.IngredientsManagement.Persistence.Factories;

using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;

public class IngredientFactory : IIngredientFactory
{
    public IIngredientModel CreateIngredient(string? name = null, byte[]? image = null, string? measureUnit = null) => new IngredientModel
    {
        Name = name ?? string.Empty,
        Image = image ?? [],
        MeasureUnit = measureUnit ?? string.Empty
    };
}
