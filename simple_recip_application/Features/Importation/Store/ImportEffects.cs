using Fluxor;
using simple_recip_application.Features.Importation.Services;
using simple_recip_application.Features.Importation.Store.Actions;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Repositories;

namespace simple_recip_application.Features.Importation.Store;

public class ImportEffects
(
    ILogger<ImportEffects> _logger,
    IServiceProvider _serviceProvider,
    IServiceScopeFactory _scopeFactory
)
{
    [EffectMethod]
    public async Task HandleStartImportAction(StartImportAction action, IDispatcher dispatcher)
    {
        try
        {
            await Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<IIngredientRepository>();

                var strategy = ImportStrategyFactory.CreateImportStrategy(action.ImportStrategy, _serviceProvider, dispatcher, repository);

                var importService = new ImportService(strategy);

                foreach (var file in action.ImportModel.FilesContent)
                {
                    if (file?.Length == 0)
                    {
                        continue;
                    }

                    var result = await importService.ExecuteImport(file!);

                    if (result.Success)
                        dispatcher.Dispatch(new ImportSuccessAction());

                    else
                        dispatcher.Dispatch(new ImportFailureAction());
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors du chargement des ingrédients");

            dispatcher.Dispatch(new ImportFailureAction());
        }
    }
}