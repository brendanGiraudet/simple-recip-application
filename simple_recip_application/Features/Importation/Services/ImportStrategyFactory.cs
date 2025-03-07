using simple_recip_application.Features.Importation.Enums;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.IngredientsManagement.Persistence.Repositories;

namespace simple_recip_application.Features.Importation.Services;

public static class ImportStrategyFactory
{
    public static IImportStrategy CreateImportStrategy(
        ImportStrategyEnum importStrategy, 
        IIngredientRepository ingredientRepository,
        ILogger<CsvImportService> _logger,
        IIngredientFactory _ingredientFactory
        )
    {
        var csvImportService = new CsvImportService(ingredientRepository, _logger, _ingredientFactory);

        return importStrategy switch
        {

            ImportStrategyEnum.ImportIngredientsFromCsv => new IngredientsFromCsvImportStrategy(csvImportService),
            _ => throw new NotImplementedException($"Strategy for {importStrategy} is not implemented.")
        };
    }
}