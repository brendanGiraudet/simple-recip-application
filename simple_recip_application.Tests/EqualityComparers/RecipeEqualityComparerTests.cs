using simple_recip_application.Features.RecipesManagement.ApplicationCore.EqualityComparers;
using simple_recip_application.Tests.Fakers;

namespace simple_recip_application.Tests.EqualityComparers;

public class RecipeEqualityComparerTests
{
    [Fact]
    public async Task ShouldTrue_WhenEquals_WithSameRecipe()
    {
        // Arrange
        var recipe = new RecipeModelFaker().Generate();
        var comparer = new RecipeEqualityComparer();

        // Act
        var isEquals = comparer.Equals(recipe, recipe);

        // Assert
        Assert.True(isEquals);
    }
    
    [Fact]
    public async Task ShouldFalse_WhenEquals_WithDifferentRecipe()
    {
         // Arrange
        var recipe = new RecipeModelFaker().Generate();
        var secondRecipe = new RecipeModelFaker().Generate();
        var comparer = new RecipeEqualityComparer();

        // Act
        var isEquals = comparer.Equals(recipe, secondRecipe);

        // Assert
        Assert.False(isEquals);
    }
}