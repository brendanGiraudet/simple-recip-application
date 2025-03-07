using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.IngredientsManagement.Persistence.Repositories;

namespace simple_recip_application.Features.Importation.Services;

public class CsvImportService
(
    IIngredientRepository _ingredientRepository,
    ILogger<CsvImportService> _logger,
    IIngredientFactory _ingredientFactory
)
{
    public async Task ImportIngredientsFromCsv(Stream fileContent)
    {
        try
        {
            using var reader = new StreamReader(fileContent);
                
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = "\t",
                BadDataFound = null
            });

            var records = csv.GetRecordsAsync<IngredientCsvRecord>();

            await foreach (var record in records)
            {

                if(string.IsNullOrEmpty(record.product_name)) continue;

                var image = await DownloadImage(record.image_url);

                if(image.Length == 0) continue;

                var ingredient = _ingredientFactory.CreateIngredient(record.product_name , image);

                await _ingredientRepository.AddAsync(ingredient);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de l'import des ingrédients");
        }
    }

    private async Task<byte[]> DownloadImage(string imageUrl)
    {
        try
        {
            if (string.IsNullOrEmpty(imageUrl))
                return [];

            using var httpClient = new HttpClient();

            var imageBytes = await httpClient.GetByteArrayAsync(imageUrl);

            return imageBytes;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de l'import de l'image de l'ingrédient : {imageUrl}");
        }

        return [];
    }
}

public class IngredientCsvRecord
{
    public string code { get; set; } = string.Empty;
    public string url { get; set; } = string.Empty;
    public string creator { get; set; } = string.Empty;
    public string created_t { get; set; } = string.Empty;
    public string created_datetime { get; set; } = string.Empty;
    public string last_modified_t { get; set; } = string.Empty;
    public string last_modified_datetime { get; set; } = string.Empty;
    public string last_modified_by { get; set; } = string.Empty;
    public string last_updated_t { get; set; } = string.Empty;
    public string last_updated_datetime { get; set; } = string.Empty;
    public string product_name { get; set; } = string.Empty;
    public string abbreviated_product_name { get; set; } = string.Empty;
    public string generic_name { get; set; } = string.Empty;
    public string quantity { get; set; } = string.Empty;
    public string packaging { get; set; } = string.Empty;
    public string packaging_tags { get; set; } = string.Empty;
    public string packaging_fr { get; set; } = string.Empty;
    public string packaging_text { get; set; } = string.Empty;
    public string brands { get; set; } = string.Empty;
    public string brands_tags { get; set; } = string.Empty;
    public string categories { get; set; } = string.Empty;
    public string categories_tags { get; set; } = string.Empty;
    public string categories_fr { get; set; } = string.Empty;
    public string origins { get; set; } = string.Empty;
    public string origins_tags { get; set; } = string.Empty;
    public string origins_fr { get; set; } = string.Empty;
    public string manufacturing_places { get; set; } = string.Empty;
    public string manufacturing_places_tags { get; set; } = string.Empty;
    public string labels { get; set; } = string.Empty;
    public string labels_tags { get; set; } = string.Empty;
    public string labels_fr { get; set; } = string.Empty;
    public string emb_codes { get; set; } = string.Empty;
    public string emb_codes_tags { get; set; } = string.Empty;
    public string first_packaging_code_geo { get; set; } = string.Empty;
    public string cities { get; set; } = string.Empty;
    public string cities_tags { get; set; } = string.Empty;
    public string purchase_places { get; set; } = string.Empty;
    public string stores { get; set; } = string.Empty;
    public string countries { get; set; } = string.Empty;
    public string countries_tags { get; set; } = string.Empty;
    public string countries_fr { get; set; } = string.Empty;
    public string ingredients_text { get; set; } = string.Empty;
    public string ingredients_tags { get; set; } = string.Empty;
    public string ingredients_analysis_tags { get; set; } = string.Empty;
    public string allergens { get; set; } = string.Empty;
    public string allergens_fr { get; set; } = string.Empty;
    public string traces { get; set; } = string.Empty;
    public string traces_tags { get; set; } = string.Empty;
    public string traces_fr { get; set; } = string.Empty;
    public string serving_size { get; set; } = string.Empty;
    public string serving_quantity { get; set; } = string.Empty;
    public string no_nutrition_data { get; set; } = string.Empty;
    public string additives_n { get; set; } = string.Empty;
    public string additives { get; set; } = string.Empty;
    public string additives_tags { get; set; } = string.Empty;
    public string additives_fr { get; set; } = string.Empty;
    public string nutriscore_score { get; set; } = string.Empty;
    public string nutriscore_grade { get; set; } = string.Empty;
    public string nova_group { get; set; } = string.Empty;
    public string pnns_groups_1 { get; set; } = string.Empty;
    public string pnns_groups_2 { get; set; } = string.Empty;
    public string food_groups { get; set; } = string.Empty;
    public string food_groups_tags { get; set; } = string.Empty;
    public string food_groups_fr { get; set; } = string.Empty;
    public string states { get; set; } = string.Empty;
    public string states_tags { get; set; } = string.Empty;
    public string states_fr { get; set; } = string.Empty;
    public string brand_owner { get; set; } = string.Empty;
    public string environmental_score_score { get; set; } = string.Empty;
    public string environmental_score_grade { get; set; } = string.Empty;
    public string nutrient_levels_tags { get; set; } = string.Empty;
    public string product_quantity { get; set; } = string.Empty;
    public string owner { get; set; } = string.Empty;
    public string data_quality_errors_tags { get; set; } = string.Empty;
    public string unique_scans_n { get; set; } = string.Empty;
    public string popularity_tags { get; set; } = string.Empty;
    public string completeness { get; set; } = string.Empty;
    public string last_image_t { get; set; } = string.Empty;
    public string last_image_datetime { get; set; } = string.Empty;
    public string main_category { get; set; } = string.Empty;
    public string main_category_fr { get; set; } = string.Empty;
    public string image_url { get; set; } = string.Empty;
    public string image_small_url { get; set; } = string.Empty;
    public string image_ingredients_url { get; set; } = string.Empty;
    public string image_ingredients_small_url { get; set; } = string.Empty;
    public string image_nutrition_url { get; set; } = string.Empty;
    public string image_nutrition_small_url { get; set; } = string.Empty;
}
