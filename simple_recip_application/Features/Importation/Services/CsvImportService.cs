using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;
using simple_recip_application.Features.IngredientsManagement.Persistence.Repositories;

namespace simple_recip_application.Features.Importation.Services;

public class CsvImportService
(
    IIngredientRepository _ingredientRepository,
    ILogger<CsvImportService> _logger
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

                var ingredient = new IngredientModel
                {
                    Name = record.product_name,
                    Image = image
                };

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
    public string code { get; set; }
    public string url { get; set; }
    public string creator { get; set; }
    public string created_t { get; set; }
    public string created_datetime { get; set; }
    public string last_modified_t { get; set; }
    public string last_modified_datetime { get; set; }
    public string last_modified_by { get; set; }
    public string last_updated_t { get; set; }
    public string last_updated_datetime { get; set; }
    public string product_name { get; set; }
    public string abbreviated_product_name { get; set; }
    public string generic_name { get; set; }
    public string quantity { get; set; }
    public string packaging { get; set; }
    public string packaging_tags { get; set; }
    public string packaging_fr { get; set; }
    public string packaging_text { get; set; }
    public string brands { get; set; }
    public string brands_tags { get; set; }
    public string categories { get; set; }
    public string categories_tags { get; set; }
    public string categories_fr { get; set; }
    public string origins { get; set; }
    public string origins_tags { get; set; }
    public string origins_fr { get; set; }
    public string manufacturing_places { get; set; }
    public string manufacturing_places_tags { get; set; }
    public string labels { get; set; }
    public string labels_tags { get; set; }
    public string labels_fr { get; set; }
    public string emb_codes { get; set; }
    public string emb_codes_tags { get; set; }
    public string first_packaging_code_geo { get; set; }
    public string cities { get; set; }
    public string cities_tags { get; set; }
    public string purchase_places { get; set; }
    public string stores { get; set; }
    public string countries { get; set; }
    public string countries_tags { get; set; }
    public string countries_fr { get; set; }
    public string ingredients_text { get; set; }
    public string ingredients_tags { get; set; }
    public string ingredients_analysis_tags { get; set; }
    public string allergens { get; set; }
    public string allergens_fr { get; set; }
    public string traces { get; set; }
    public string traces_tags { get; set; }
    public string traces_fr { get; set; }
    public string serving_size { get; set; }
    public string serving_quantity { get; set; }
    public string no_nutrition_data { get; set; }
    public string additives_n { get; set; }
    public string additives { get; set; }
    public string additives_tags { get; set; }
    public string additives_fr { get; set; }
    public string nutriscore_score { get; set; }
    public string nutriscore_grade { get; set; }
    public string nova_group { get; set; }
    public string pnns_groups_1 { get; set; }
    public string pnns_groups_2 { get; set; }
    public string food_groups { get; set; }
    public string food_groups_tags { get; set; }
    public string food_groups_fr { get; set; }
    public string states { get; set; }
    public string states_tags { get; set; }
    public string states_fr { get; set; }
    public string brand_owner { get; set; }
    public string environmental_score_score { get; set; }
    public string environmental_score_grade { get; set; }
    public string nutrient_levels_tags { get; set; }
    public string product_quantity { get; set; }
    public string owner { get; set; }
    public string data_quality_errors_tags { get; set; }
    public string unique_scans_n { get; set; }
    public string popularity_tags { get; set; }
    public string completeness { get; set; }
    public string last_image_t { get; set; }
    public string last_image_datetime { get; set; }
    public string main_category { get; set; }
    public string main_category_fr { get; set; }
    public string image_url { get; set; }
    public string image_small_url { get; set; }
    public string image_ingredients_url { get; set; }
    public string image_ingredients_small_url { get; set; }
    public string image_nutrition_url { get; set; }
    public string image_nutrition_small_url { get; set; }
}
