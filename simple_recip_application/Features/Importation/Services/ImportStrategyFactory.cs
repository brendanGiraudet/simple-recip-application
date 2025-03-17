using Fluxor;
using simple_recip_application.Features.Importation.Enums;
using simple_recip_application.Features.IngredientsManagement.Persistence.Repositories;

namespace simple_recip_application.Features.Importation.Services;

public static class ImportStrategyFactory
{
    public static IImportStrategy CreateImportStrategy(
        ImportStrategyEnum _importStrategy,
        IServiceProvider _serviceProvider,
        IDispatcher _dispatcher,
        IIngredientRepository _ingredientRepository
        )
    {
        return _importStrategy switch
        {
            ImportStrategyEnum.ImportIngredientsFromCsv => new IngredientsFromCsvImportStrategy(_serviceProvider),

            ImportStrategyEnum.RecipesFromHelloFreshPicture => new RecipesFromPictureStrategy(_serviceProvider, _dispatcher, _ingredientRepository),

            _ => throw new NotImplementedException($"Strategy for {_importStrategy} is not implemented.")
        };
    }
}