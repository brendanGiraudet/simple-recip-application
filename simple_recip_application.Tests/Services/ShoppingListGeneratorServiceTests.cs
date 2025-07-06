using Microsoft.Extensions.Logging;
using Moq;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Factories;
using simple_recip_application.Features.ShoppingListManagement.ApplicationCore.Services;
using simple_recip_application.Features.ShoppingListManagement.Persistence.Services;
using simple_recip_application.Tests.Fakers;

namespace simple_recip_application.Tests;

public class ShoppingListGeneratorServiceTests
{
    ILogger<ShoppingListGeneratorService> LoggerMock => new Mock<ILogger<ShoppingListGeneratorService>>().Object;
    IShoppingListItemModelFactory ShoppingListItemModelFactoryMock => new Mock<IShoppingListItemModelFactory>().Object;
    IRecipeRepository RecipeRepository => new Mock<IRecipeRepository>().Object;

    IShoppingListGeneratorService GetShoppingListGeneratorService() => new ShoppingListGeneratorService(LoggerMock, ShoppingListItemModelFactoryMock, RecipeRepository);

    [Fact]
    public async Task ShouldMethodResultSuccessAndNotEmptyCsvContent_WhenGenerateShoppingListCsvContentAsync()
    {
        // Arrange
        var recipes = new RecipeModelFaker().Generate(2);
        var service = GetShoppingListGeneratorService();

        // Act
        var result = await service.GenerateShoppingListCsvContentAsync(recipes);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Item);
        Assert.NotEmpty(result.Item);
    }
}
