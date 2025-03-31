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

    IOpenAiDataAnalysisService GetOpenAiDataAnalysisService(IHttpClientFactory? httpClientFactory = null) => new OpenAiDataAnalysisService(moqOfOpenAiDataAnalysisService, GetOpenApisettingsMock(), httpClientFactory ?? GetHttpClientFactoryMock());

    [Fact]
    public async Task ShouldNotEmptyRecipeText_WhenExtractTextFromImageAsync_WithRecipeImage()
    {
        // Arrange
        byte[] image = [];
        var service = GetOpenAiDataAnalysisService();

        // Act
        var result = await service.ExtractTextFromImageAsync(image);

        // Assert
        Assert.NotEmpty(result);
    }
    
    [Fact]
    public async Task ShouldNotEmptyRecipeText_WhenExtractTextFromImageAsync_WithSpecifiqueText()
    {
        // Arrange
        byte[] image = [];

        var chatCompletionUrl = GetOpenApisettingsMock().Value.ChatCompletionUrl;

        var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        var jsonContent = "{\r\n  \"id\": \"chatcmpl-BHDHvgxiDXri537LbJFuqFe5xzuQM\",\r\n  \"object\": \"chat.completion\",\r\n  \"created\": 1743442355,\r\n  \"model\": \"gpt-4o-2024-08-06\",\r\n  \"choices\": [\r\n    {\r\n      \"index\": 0,\r\n      \"message\": {\r\n        \"role\": \"assistant\",\r\n        \"content\": \"```json\\n{\\n  \\\"Name\\\": \\\"Salade de Pois Chiches et Légumes\\\",\\n  \\\"Description\\\": \\\"Une salade saine et savoureuse avec des pois chiches et des légumes variés.\\\",\\n  \\\"IngredientModels\\\": [\\n    {\\n      \\\"RecipeModel\\\": null,\\n      \\\"IngredientModel\\\": {\\n        \\\"Name\\\": \\\"Pois chiches\\\",\\n        \\\"Image\\\": null,\\n        \\\"MeasureUnit\\\": \\\"boîte\\\"\\n      },\\n      \\\"Quantity\\\": 1\\n    },\\n    {\\n      \\\"RecipeModel\\\": null,\\n      \\\"IngredientModel\\\": {\\n        \\\"Name\\\": \\\"Oignons rouges\\\",\\n        \\\"Image\\\": null,\\n        \\\"MeasureUnit\\\": \\\"pièce\\\"\\n      },\\n      \\\"Quantity\\\": 1\\n    },\\n    {\\n      \\\"RecipeModel\\\": null,\\n      \\\"IngredientModel\\\": {\\n        \\\"Name\\\": \\\"Poivron\\\",\\n        \\\"Image\\\": null,\\n        \\\"MeasureUnit\\\": \\\"pièce\\\"\\n      },\\n      \\\"Quantity\\\": 1\\n    },\\n    {\\n      \\\"RecipeModel\\\": null,\\n      \\\"IngredientModel\\\": {\\n        \\\"Name\\\": \\\"Coriandre\\\",\\n        \\\"Image\\\": null,\\n        \\\"MeasureUnit\\\": \\\"sachet\\\"\\n      },\\n      \\\"Quantity\\\": 1\\n    },\\n    {\\n      \\\"RecipeModel\\\": null,\\n      \\\"IngredientModel\\\": {\\n        \\\"Name\\\": \\\"Citron\\\",\\n        \\\"Image\\\": null,\\n        \\\"MeasureUnit\\\": \\\"pièce\\\"\\n      },\\n      \\\"Quantity\\\": 1\\n    },\\n    {\\n      \\\"RecipeModel\\\": null,\\n      \\\"IngredientModel\\\": {\\n        \\\"Name\\\": \\\"Fromage à la grecque\\\",\\n        \\\"Image\\\": null,\\n        \\\"MeasureUnit\\\": \\\"cuillère\\\"\\n      },\\n      \\\"Quantity\\\": 1\\n    },\\n    {\\n      \\\"RecipeModel\\\": null,\\n      \\\"IngredientModel\\\": {\\n        \\\"Name\\\": \\\"Yaourt\\\",\\n        \\\"Image\\\": null,\\n        \\\"MeasureUnit\\\": \\\"sachet\\\"\\n      },\\n      \\\"Quantity\\\": 1\\n    },\\n    {\\n      \\\"RecipeModel\\\": null,\\n      \\\"IngredientModel\\\": {\\n        \\\"Name\\\": \\\"Avocat\\\",\\n        \\\"Image\\\": null,\\n        \\\"MeasureUnit\\\": \\\"pièce\\\"\\n      },\\n      \\\"Quantity\\\": 1\\n    },\\n    {\\n      \\\"RecipeModel\\\": null,\\n      \\\"IngredientModel\\\": {\\n        \\\"Name\\\": \\\"Huile d'olive\\\",\\n        \\\"Image\\\": null,\\n        \\\"MeasureUnit\\\": \\\"cuillère\\\"\\n      },\\n      \\\"Quantity\\\": 3\\n    }\\n  ],\\n  \\\"Instructions\\\": \\\"1. Dorer les pois chiches: Rincer et égoutter les pois chiches, puis les sécher. Chauffer l'huile dans une poêle et faire sauter les pois chiches jusqu'à ce qu'ils soient dorés. \\\\\\n  2. Couper: Émincer l'oignon en demi-lunes, couper le poivron en fines lanières. Presser le citron. Hacher la coriandre. \\\\\\n  3. Cuire les légumes: Ajouter l'oignon et le poivron avec les pois chiches, saler et poivrer. Cuire en remuant. \\\\\\n  4. Faire les sauces: Mélanger l'huile d'olive, le jus de citron, sel, et poivre pour la vinaigrette. Mélanger le yaourt avec le fromage. \\\\\\n  5. Mélanger: Mélanger la salade, ajouter la coriandre. \\\\\\n  6. Servir: Répartir dans les assiettes avec de l'avocat.\\\",\\n  \\\"PreparationTime\\\": \\\"00:15\\\",\\n  \\\"CookingTime\\\": \\\"00:10\\\",\\n  \\\"Image\\\": null,\\n  \\\"Category\\\": \\\"Salade\\\"\\n}\\n```\",\r\n        \"refusal\": null,\r\n        \"annotations\": []\r\n      },\r\n      \"logprobs\": null,\r\n      \"finish_reason\": \"stop\"\r\n    }\r\n  ],\r\n  \"usage\": {\r\n    \"prompt_tokens\": 1015,\r\n    \"completion_tokens\": 754,\r\n    \"total_tokens\": 1769,\r\n    \"prompt_tokens_details\": {\r\n      \"cached_tokens\": 0,\r\n      \"audio_tokens\": 0\r\n    },\r\n    \"completion_tokens_details\": {\r\n      \"reasoning_tokens\": 0,\r\n      \"audio_tokens\": 0,\r\n      \"accepted_prediction_tokens\": 0,\r\n      \"rejected_prediction_tokens\": 0\r\n    }\r\n  },\r\n  \"service_tier\": \"default\",\r\n  \"system_fingerprint\": \"fp_de57b65c90\"\r\n}";

        var mockResponse = new HttpResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
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

        var service = GetOpenAiDataAnalysisService(httpClientFactory: httpclientFactoryMock.Object);

        // Act
        var result = await service.ExtractTextFromImageAsync(image);

        // Assert
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task ShouldRecipeNotNull_WhenExtractRecipeFromImageAsync_WithRecipeImage()
    {
        // Arrange
        byte[] image = [];
        var service = GetOpenAiDataAnalysisService();

        // Act
        var result = await service.ExtractRecipeFromImageAsync(image);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task ShouldNotEmptyRecipeText_WhenExtractRecipeFromImageAsync_WithSpecifiqueText()
    {
        // Arrange
        byte[] image = [];

        var chatCompletionUrl = GetOpenApisettingsMock().Value.ChatCompletionUrl;

        var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        var jsonContent = "{\r\n  \"id\": \"chatcmpl-BHDHvgxiDXri537LbJFuqFe5xzuQM\",\r\n  \"object\": \"chat.completion\",\r\n  \"created\": 1743442355,\r\n  \"model\": \"gpt-4o-2024-08-06\",\r\n  \"choices\": [\r\n    {\r\n      \"index\": 0,\r\n      \"message\": {\r\n        \"role\": \"assistant\",\r\n        \"content\": \"```json\\n{\\n  \\\"Name\\\": \\\"Salade de Pois Chiches et Légumes\\\",\\n  \\\"Description\\\": \\\"Une salade saine et savoureuse avec des pois chiches et des légumes variés.\\\",\\n  \\\"IngredientModels\\\": [\\n    {\\n      \\\"RecipeModel\\\": null,\\n      \\\"IngredientModel\\\": {\\n        \\\"Name\\\": \\\"Pois chiches\\\",\\n        \\\"Image\\\": null,\\n        \\\"MeasureUnit\\\": \\\"boîte\\\"\\n      },\\n      \\\"Quantity\\\": 1\\n    },\\n    {\\n      \\\"RecipeModel\\\": null,\\n      \\\"IngredientModel\\\": {\\n        \\\"Name\\\": \\\"Oignons rouges\\\",\\n        \\\"Image\\\": null,\\n        \\\"MeasureUnit\\\": \\\"pièce\\\"\\n      },\\n      \\\"Quantity\\\": 1\\n    },\\n    {\\n      \\\"RecipeModel\\\": null,\\n      \\\"IngredientModel\\\": {\\n        \\\"Name\\\": \\\"Poivron\\\",\\n        \\\"Image\\\": null,\\n        \\\"MeasureUnit\\\": \\\"pièce\\\"\\n      },\\n      \\\"Quantity\\\": 1\\n    },\\n    {\\n      \\\"RecipeModel\\\": null,\\n      \\\"IngredientModel\\\": {\\n        \\\"Name\\\": \\\"Coriandre\\\",\\n        \\\"Image\\\": null,\\n        \\\"MeasureUnit\\\": \\\"sachet\\\"\\n      },\\n      \\\"Quantity\\\": 1\\n    },\\n    {\\n      \\\"RecipeModel\\\": null,\\n      \\\"IngredientModel\\\": {\\n        \\\"Name\\\": \\\"Citron\\\",\\n        \\\"Image\\\": null,\\n        \\\"MeasureUnit\\\": \\\"pièce\\\"\\n      },\\n      \\\"Quantity\\\": 1\\n    },\\n    {\\n      \\\"RecipeModel\\\": null,\\n      \\\"IngredientModel\\\": {\\n        \\\"Name\\\": \\\"Fromage à la grecque\\\",\\n        \\\"Image\\\": null,\\n        \\\"MeasureUnit\\\": \\\"cuillère\\\"\\n      },\\n      \\\"Quantity\\\": 1\\n    },\\n    {\\n      \\\"RecipeModel\\\": null,\\n      \\\"IngredientModel\\\": {\\n        \\\"Name\\\": \\\"Yaourt\\\",\\n        \\\"Image\\\": null,\\n        \\\"MeasureUnit\\\": \\\"sachet\\\"\\n      },\\n      \\\"Quantity\\\": 1\\n    },\\n    {\\n      \\\"RecipeModel\\\": null,\\n      \\\"IngredientModel\\\": {\\n        \\\"Name\\\": \\\"Avocat\\\",\\n        \\\"Image\\\": null,\\n        \\\"MeasureUnit\\\": \\\"pièce\\\"\\n      },\\n      \\\"Quantity\\\": 1\\n    },\\n    {\\n      \\\"RecipeModel\\\": null,\\n      \\\"IngredientModel\\\": {\\n        \\\"Name\\\": \\\"Huile d'olive\\\",\\n        \\\"Image\\\": null,\\n        \\\"MeasureUnit\\\": \\\"cuillère\\\"\\n      },\\n      \\\"Quantity\\\": 3\\n    }\\n  ],\\n  \\\"Instructions\\\": \\\"1. Dorer les pois chiches: Rincer et égoutter les pois chiches, puis les sécher. Chauffer l'huile dans une poêle et faire sauter les pois chiches jusqu'à ce qu'ils soient dorés. \\\\\\n  2. Couper: Émincer l'oignon en demi-lunes, couper le poivron en fines lanières. Presser le citron. Hacher la coriandre. \\\\\\n  3. Cuire les légumes: Ajouter l'oignon et le poivron avec les pois chiches, saler et poivrer. Cuire en remuant. \\\\\\n  4. Faire les sauces: Mélanger l'huile d'olive, le jus de citron, sel, et poivre pour la vinaigrette. Mélanger le yaourt avec le fromage. \\\\\\n  5. Mélanger: Mélanger la salade, ajouter la coriandre. \\\\\\n  6. Servir: Répartir dans les assiettes avec de l'avocat.\\\",\\n  \\\"PreparationTime\\\": \\\"00:15\\\",\\n  \\\"CookingTime\\\": \\\"00:10\\\",\\n  \\\"Image\\\": null,\\n  \\\"Category\\\": \\\"Salade\\\"\\n}\\n```\",\r\n        \"refusal\": null,\r\n        \"annotations\": []\r\n      },\r\n      \"logprobs\": null,\r\n      \"finish_reason\": \"stop\"\r\n    }\r\n  ],\r\n  \"usage\": {\r\n    \"prompt_tokens\": 1015,\r\n    \"completion_tokens\": 754,\r\n    \"total_tokens\": 1769,\r\n    \"prompt_tokens_details\": {\r\n      \"cached_tokens\": 0,\r\n      \"audio_tokens\": 0\r\n    },\r\n    \"completion_tokens_details\": {\r\n      \"reasoning_tokens\": 0,\r\n      \"audio_tokens\": 0,\r\n      \"accepted_prediction_tokens\": 0,\r\n      \"rejected_prediction_tokens\": 0\r\n    }\r\n  },\r\n  \"service_tier\": \"default\",\r\n  \"system_fingerprint\": \"fp_de57b65c90\"\r\n}";

        var mockResponse = new HttpResponseMessage
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
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

        var service = GetOpenAiDataAnalysisService(httpClientFactory: httpclientFactoryMock.Object);

        // Act
        var result = await service.ExtractRecipeFromImageAsync(image);

        // Assert
        Assert.NotNull(result);
    }
}
