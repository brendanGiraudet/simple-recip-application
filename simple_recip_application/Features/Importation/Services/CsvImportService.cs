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
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

            var records = csv.GetRecords<IngredientCsvRecord>().ToHashSet();

            foreach (var record in records)
            {
                var ingredient = new IngredientModel
                {
                    Name = record.Name,
                    Image = await DownloadImage(record.ImageUrl)
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
    public string Name { get; set; }
    public string ImageUrl { get; set; }
}