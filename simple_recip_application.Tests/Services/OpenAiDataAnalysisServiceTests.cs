using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using simple_recip_application.Services;

namespace simple_recip_application.Tests;

public class OpenAiDataAnalysisServiceTests
{
    ILogger<OpenAiDataAnalysisService> moqOfOpenAiDataAnalysisService => new Mock<ILogger<OpenAiDataAnalysisService>>().Object;

    IConfiguration GetConfigurationMock()
    {
        var inMemorySettings = new Dictionary<string, string> {
            {"OpenAiApiKey", "apikey "},
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
    }

    [Fact]
    public async Task ShouldNotEmptyRecipeText_WhenExtractTextFromImageAsync_WithRecipeImage()
    {
        // Arrange
        var imageFilepath = "/home/bakayarusama/Téléchargements/recette_hellofresh.jpg";
        byte[] image = File.ReadAllBytes(imageFilepath);
        var service = new OpenAiDataAnalysisService(moqOfOpenAiDataAnalysisService, GetConfigurationMock());

        // Act
        var result = await service.ExtractTextFromImageAsync(image);

        // Assert
        Assert.NotEmpty(result);
    }
    
    [Fact]
    public async Task ShouldRecipeNotNull_WhenExtractRecipeFromImageAsync_WithRecipeImage()
    {
        // Arrange
        var imageFilepath = "/home/bakayarusama/Téléchargements/recette_hellofresh.jpg";
        byte[] image = File.ReadAllBytes(imageFilepath);
        var service = new OpenAiDataAnalysisService(moqOfOpenAiDataAnalysisService, GetConfigurationMock());

        // Act
        var result = await service.ExtractRecipeFromImageAsync(image);

        // Assert
        Assert.NotNull(result);
    }
}
