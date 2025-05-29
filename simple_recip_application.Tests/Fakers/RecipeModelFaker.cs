using Bogus;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;

namespace simple_recip_application.Tests.Fakers;

public class RecipeModelFaker : Faker<RecipeModel>
{
    public RecipeModelFaker()
    {
        RuleFor(c => c.Category, f => f.Random.String2(10));
        RuleFor(c => c.CookingTime, f => f.Date.SoonTimeOnly());
        RuleFor(c => c.PreparationTime, f => f.Date.SoonTimeOnly());
        RuleFor(c => c.Description, f => f.Random.String2(10));
        RuleFor(c => c.Name, f => f.Random.String2(10));
        RuleFor(c => c.Ingredients, f => new RecipeIngredientModelFaker().Generate(2));
        RuleFor(c => c.Image, f => f.Random.Bytes(9999999));
    }
}