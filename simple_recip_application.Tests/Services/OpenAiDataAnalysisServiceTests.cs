using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using simple_recip_application.Services;
using simple_recip_application.Settings;
using simple_recip_application.Tests.Fakers;

namespace simple_recip_application.Tests;

public class OpenAiDataAnalysisServiceTests
{
    ILogger<OpenAiDataAnalysisService> moqOfOpenAiDataAnalysisService => new Mock<ILogger<OpenAiDataAnalysisService>>().Object;

    IOptions<OpenApisettings> GetOpenApisettingsMock()
    {
        var mock = new Mock<IOptions<OpenApisettings>>();

        mock.Setup(c => c.Value)
            .Returns(new OpenApisettings
            {
                ApiKey = "apikey",
                ChatCompletionUrl = "https://api.openai.com/v1/chat/completions"
            });

        return mock.Object;
    }

    IHttpClientFactory GetHttpClientFactoryMock()
    {
        var chatCompletionUrl = GetOpenApisettingsMock().Value.ChatCompletionUrl;

        var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        var mockResponse = new HttpResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(new OpenApiReturnFaker().Generate()), Encoding.UTF8, "application/json")
        };

        mockHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>
            (
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(
                    m => m.Method == HttpMethod.Post &&
                    string.Equals(m.RequestUri.AbsoluteUri, chatCompletionUrl, StringComparison.InvariantCultureIgnoreCase)
                ),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(mockResponse);

        var httpClient = new HttpClient(mockHandler.Object);

        var httpclientFactoryMock = new Mock<IHttpClientFactory>();

        httpclientFactoryMock.Setup(c => c.CreateClient(It.IsAny<string>()))
                             .Returns(httpClient);

        return httpclientFactoryMock.Object;
    }

    IOpenAiDataAnalysisService GetOpenAiDataAnalysisService() => new OpenAiDataAnalysisService(moqOfOpenAiDataAnalysisService, GetOpenApisettingsMock(), GetHttpClientFactoryMock());

    [Fact]
    public async Task ShouldNotEmptyRecipeText_WhenExtractTextFromImageAsync_WithRecipeImage()
    {
        // Arrange
        var imageFilepath = "/home/bakayarusama/Téléchargements/recette_hellofresh.jpg";
        byte[] image = File.ReadAllBytes(imageFilepath);
        var service = GetOpenAiDataAnalysisService();

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
        var service = GetOpenAiDataAnalysisService();

        // Act
        var result = await service.ExtractRecipeFromImageAsync(image);

        // Assert
        Assert.NotNull(result);
    }
}
