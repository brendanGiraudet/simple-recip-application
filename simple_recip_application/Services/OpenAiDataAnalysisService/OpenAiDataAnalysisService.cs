using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using simple_recip_application.Constants;
using simple_recip_application.Dtos;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.Converters;
using simple_recip_application.Settings;

namespace simple_recip_application.Services;

public class OpenAiDataAnalysisService
(
    ILogger<OpenAiDataAnalysisService> _logger,
    IOptions<OpenApisettings> _openApisettingsOptions,
    IHttpClientFactory _httpClientFactory
)
 : IOpenAiDataAnalysisService
{
    OpenApisettings _openApisettings => _openApisettingsOptions.Value;

    public async Task<string> ExtractTextFromImageAsync(byte[] imageData)
    {
        try
        {
            var apiKey = _openApisettings.ApiKey;

            var base64Image = Convert.ToBase64String(imageData);

            var requestBody = new
            {
                model = "gpt-4o",
                messages = new object[]
                {
                new
                {
                    role = "user",
                    content = new object[]
                    {
                        new { type = "text", text = textPrompt },
                        new
                        {
                            type = "image_url",
                            image_url = new
                            {
                                url = $"data:image/jpeg;base64,{base64Image}"
                            }
                        }
                    }
                }
                },
                max_tokens = 1000
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient(HttpClientNamesConstants.OpenApi);

            using var response = await httpClient.PostAsync(_openApisettings.ChatCompletionUrl, jsonContent);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Erreur: {response.StatusCode}, Détail: {response.Content.ReadAsStringAsync().Result}");

            var responseString = await response.Content.ReadAsStringAsync();

            var responseData = JsonSerializer.Deserialize<JsonElement>(responseString);

            return GetResponseData(responseBody: responseData);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'analyse de l'image");
        }

        return string.Empty;
    }

    private static string GetResponseData(JsonElement responseBody)
    {
        return responseBody
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content").GetString() ?? "";
    }

    private string textPrompt => @"Peux tu transcrire cette image et me ressortir la recette et les ingrédients, ensuite après avoir ressortit la recette peux tu parser la recette en json grace au class C# suivante public interface IRecipeModel : IEntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<IRecipeIngredientModel> IngredientModels { get; set; }
    public string Instructions { get; set; }
    public TimeOnly PreparationTime { get; set; }
    public TimeOnly CookingTime { get; set; }
    public byte[] Image { get; set; }
    public string Category { get; set; }
} public interface IRecipeIngredientModel
{
    public IRecipeModel RecipeModel { get; set; }

    public IIngredientModel IngredientModel { get; set; }

    public decimal Quantity { get; set; }
} public interface IIngredientModel : IEntityBase
{
    public string Name { get; set; }
    public byte[] Image { get; set; }
    public string MeasureUnit { get; set; }
} au final, je souhaite avoir seulement le resultat en json pur et rien d'autre";

    public async Task<MethodResult<IRecipeModel?>> ExtractRecipeFromImageAsync(byte[] imageData)
    {
        try
        {
            var text = await ExtractTextFromImageAsync(imageData);

            // Suppression de "```json" du debut et "```" de fin
            var beginJsonMarkdownText = "```json";
            var endJsonMarkdownText = "```";
            
            if(text.Contains(beginJsonMarkdownText))
                text = text.Remove(text.IndexOf(beginJsonMarkdownText), beginJsonMarkdownText.Length);
            
            if(text.Contains(endJsonMarkdownText))
                text = text.Remove(text.IndexOf(endJsonMarkdownText), endJsonMarkdownText.Length);

                var recipe = JsonSerializer.Deserialize<IRecipeModel>(text, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new RecipeModelJsonConverter(),
                    new RecipeIngredientModelJsonConverter(),
                    new IngredientModelJsonConverter()
                },
                PropertyNameCaseInsensitive = true
            });

            return new MethodResult<IRecipeModel?>(true, recipe);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'extraction de la recette depuis l'image");

            return new MethodResult<IRecipeModel?>(false, null);
        }
    }
}