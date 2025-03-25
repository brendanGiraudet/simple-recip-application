using simple_recip_application.Features.IngredientsManagement.ApplicationCore.EqualityComparers;
using simple_recip_application.Tests.Fakers;

namespace simple_recip_application.Tests.EqualityComparers;

public class IngredientEqualityComparerTests
{
    [Fact]
    public async Task ShouldTrue_WhenEquals_WithSameIngredient()
    {
        // Arrange
        var ingredient = new IngredientModelFaker().Generate();
        var comparer = new IngredientEqualityComparer();

        // Act
        var isEquals = comparer.Equals(ingredient, ingredient);

        // Assert
        Assert.True(isEquals);
    }
    
    [Fact]
    public async Task ShouldFalse_WhenEquals_WithDifferentIngredient()
    {
        // Arrange
        var ingredient = new IngredientModelFaker().Generate();
        var secondIngredient = new IngredientModelFaker().Generate();
        var comparer = new IngredientEqualityComparer();

        // Act
        var isEquals = comparer.Equals(ingredient, secondIngredient);

        // Assert
        Assert.False(isEquals);
    }
}