using System.Linq.Expressions;
using Fluxor;
using Microsoft.Extensions.Logging;
using Moq;
using simple_recip_application.Dtos;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Factories;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Services;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Services;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.UserInfos.Store;
using simple_recip_application.Tests.Fakers;

namespace simple_recip_application.Tests;

public class RecipePlanifierServiceTests
{
    private readonly Mock<ILogger<RecipePlanifierService>> _loggerMock = new();
    private readonly Mock<IRecipeRepository> _recipeRepositoryMock = new();
    private readonly Mock<IPlanifiedRecipeModelFactory> _planifiedRecipeModelFactoryMock = new();
    private readonly Mock<ICalendarRepository> _calendarRepositoryMock = new();
    private IState<UserInfosState> CreateUserInfosState()
    {
        var mock = new Mock<IState<UserInfosState>>();

        mock.Setup(s => s.Value)
            .Returns(new UserInfosState
            {
                UserInfo = new UserInfosModelFaker().Generate()
            });

        return mock.Object;
    }

    private IRecipePlanifierService GetRecipePlanifierService() => new RecipePlanifierService(
        _loggerMock.Object,
        _recipeRepositoryMock.Object,
        _planifiedRecipeModelFactoryMock.Object,
        CreateUserInfosState(),
        _calendarRepositoryMock.Object);

    [Fact]
    public async Task ShouldMethodResultSuccessAndNotEmptyDictionary_WhenGetPlanifiedRecipesForTheWeek()
    {
        // Arrange
        var currentWeekDate = DateTime.UtcNow;

        var recipes = new RecipeModelFaker().Generate(7);

        _recipeRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Expression<Func<IRecipeModel, bool>>>(), It.IsAny<Expression<Func<IRecipeModel, object>>>()))
            .ReturnsAsync(new MethodResult<IEnumerable<IRecipeModel>>(true, recipes));

        _planifiedRecipeModelFactoryMock
            .Setup(factory => factory.CreatePlanifiedRecipeModel(It.IsAny<IRecipeModel>(), It.IsAny<DateTime>(), It.IsAny<Guid>(), It.IsAny<string>(), null))
            .Returns(new PlanifiedRecipeModelFaker().Generate());

        var service = GetRecipePlanifierService();

        // Act
        var result = await service.GetPlanifiedRecipesForTheWeekAsync(currentWeekDate);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Item);
        Assert.Equal(7, result.Item.Count);

        foreach (var day in Enum.GetValues<DayOfWeek>())
        {
            Assert.True(result.Item.ContainsKey(day));
            Assert.NotEmpty(result.Item[day]);

            var plannedRecipe = result.Item[day].First();
            Assert.NotNull(plannedRecipe.RecipeModel);
        }
    }
}
