using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.EqualityComparers;
using simple_recip_application.Tests.Fakers;

namespace simple_recip_application.Tests.EqualityComparers;

public class PlanifiedRecipeEqualityComparerTests
{
    [Fact]
    public async Task ShouldTrue_WhenEquals_WithSamePlanifiedRecipe()
    {
        // Arrange
        var planifiedRecipe = new PlanifiedRecipeModelFaker().Generate();
        var comparer = new PlanifiedRecipeEqualityComparer();

        // Act
        var isEquals = comparer.Equals(planifiedRecipe, planifiedRecipe);

        // Assert
        Assert.True(isEquals);
    }
    
    [Fact]
    public async Task ShouldFalse_WhenEquals_WithDifferentPlanifiedRecipe()
    {
        // Arrange
        var planifiedRecipe = new PlanifiedRecipeModelFaker().Generate();
        var secondPlanifiedRecipe = new PlanifiedRecipeModelFaker().Generate();
        var comparer = new PlanifiedRecipeEqualityComparer();

        // Act
        var isEquals = comparer.Equals(planifiedRecipe, secondPlanifiedRecipe);

        // Assert
        Assert.False(isEquals);
    }
}