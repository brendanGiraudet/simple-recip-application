using Bogus;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;

namespace simple_recip_application.Tests.Fakers;

public class IngredientModelFaker : Faker<IngredientModel>
{
    public IngredientModelFaker()
    {
        RuleFor(c => c.MeasureUnit, f => f.Random.String2(10));
        RuleFor(c => c.Name, f => f.Random.String2(10));
    }
}