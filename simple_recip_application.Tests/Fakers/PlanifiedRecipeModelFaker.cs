using Bogus;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Entities;

namespace simple_recip_application.Tests.Fakers;

public class PlanifiedRecipeModelFaker : Faker<PlanifiedRecipeModel>
{
    public PlanifiedRecipeModelFaker()
    {
        RuleFor(c => c.MomentOftheDay, f => f.Random.String2(10));
        RuleFor(c => c.PlanifiedDateTime, f => f.Date.Future());
        RuleFor(c => c.RecipeId, f => Guid.NewGuid());
        RuleFor(c => c.UserId, f => f.Random.String2(10));
        RuleFor(c => c.RecipeModel, new RecipeModelFaker().Generate());
    }
}