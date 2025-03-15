using simple_recip_application.Features.Importation.Enums;

namespace simple_recip_application.Features.Importation.Services;

public static class ImportStrategyFactory
{
    public static IImportStrategy CreateImportStrategy(
        ImportStrategyEnum _importStrategy,
        IServiceProvider _serviceProvider
        )
    {
        return _importStrategy switch
        {
            ImportStrategyEnum.ImportIngredientsFromCsv => new IngredientsFromCsvImportStrategy(_serviceProvider),
            ImportStrategyEnum.RecipesFromHelloFreshPicture => new RecipesFromPictureStrategy(_serviceProvider),
            _ => throw new NotImplementedException($"Strategy for {_importStrategy} is not implemented.")
        };
    }
}