using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.Converters;

namespace simple_recip_application.Services;

public class OpenAiDataAnalysisService
(
    ILogger<OpenAiDataAnalysisService> _logger,
    IConfiguration _configuration
)
 : IOpenAiDataAnalysisService
{

    public async Task<string> ExtractTextFromImageAsync(byte[] imageData)
    {
        try
        {
            var apiKey = _configuration["OpenAiApiKey"];

            var base64Image = Convert.ToBase64String(imageData);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

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

            using var response = await GetHttpClient(apiKey).PostAsync("https://api.openai.com/v1/chat/completions", jsonContent);

            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Erreur: {response.StatusCode}, Détail: {response.Content.ReadAsStringAsync().Result}");

            var responseData = JsonSerializer.Deserialize<JsonElement>(await response.Content.ReadAsStringAsync());

            return GetResponseData(responseBody: responseData);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'analyse de l'image");
        }

        return string.Empty;
    }

    private static HttpClient GetHttpClient(string apiKey)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        return client;
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
    public Guid RecipeId { get; set; }
    public IRecipeModel RecipeModel { get; set; }

    public Guid IngredientId { get; set; }
    public IIngredientModel IngredientModel { get; set; }

    public decimal Quantity { get; set; }
} public interface IIngredientModel : IEntityBase
{
    public string Name { get; set; }
    public byte[] Image { get; set; }
    public string MeasureUnit { get; set; }
} au final, je souhaite avoir seulement le resultat du json et rien d'autre";

    public async Task<IRecipeModel?> ExtractRecipeFromImageAsync(byte[] imageData)
    {
        try
        {
            var text = await ExtractTextFromImageAsync(imageData);
            
            // Suppression de ```json du debut et ```de fin
            var temp = text.Substring(7).Replace('`', ' ').Trim();

            return JsonSerializer.Deserialize<IRecipeModel>(temp, new JsonSerializerOptions
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
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'extraction de la recette depuis l'image");
        }

        return null;
    }
}