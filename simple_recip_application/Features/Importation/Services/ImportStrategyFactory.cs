using simple_recip_application.Features.Importation.Enums;
using simple_recip_application.Features.IngredientsManagement.Persistence.Repositories;

namespace simple_recip_application.Features.Importation.Services;

public static class ImportStrategyFactory
{
    public static IImportStrategy CreateImportStrategy(
        ImportStrategyEnum importStrategy, 
        IIngredientRepository ingredientRepository,
        ILogger<CsvImportService> _logger
        )
    {
        var csvImportService = new CsvImportService(ingredientRepository, _logger);

        return importStrategy switch
        {

            ImportStrategyEnum.ImportIngredientsFromCsv => new IngredientsFromCsvImportStrategy(csvImportService),
            _ => throw new NotImplementedException($"Strategy for {importStrategy} is not implemented.")
        };
    }
}