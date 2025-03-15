using Fluxor;
using simple_recip_application.Features.Importation.Enums;

namespace simple_recip_application.Features.Importation.Services;

public static class ImportStrategyFactory
{
    public static IImportStrategy CreateImportStrategy(
        ImportStrategyEnum _importStrategy,
        IServiceProvider _serviceProvider,
        IDispatcher _dispatcher
        )
    {
        return _importStrategy switch
        {
            ImportStrategyEnum.ImportIngredientsFromCsv => new IngredientsFromCsvImportStrategy(_serviceProvider),

            ImportStrategyEnum.RecipesFromHelloFreshPicture => new RecipesFromPictureStrategy(_serviceProvider, _dispatcher),
            
            _ => throw new NotImplementedException($"Strategy for {_importStrategy} is not implemented.")
        };
    }
}