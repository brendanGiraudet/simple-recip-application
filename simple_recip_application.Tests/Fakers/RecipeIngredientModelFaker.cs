using Bogus;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;

namespace simple_recip_application.Tests.Fakers;

public class RecipeIngredientModelFaker : Faker<RecipeIngredientModel>
{
    public RecipeIngredientModelFaker()
    {
        RuleFor( c=> c.IngredientModel, f => new IngredientModelFaker().Generate());
    }
}