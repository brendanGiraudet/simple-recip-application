using Fluxor;
using Microsoft.Extensions.Localization;
using simple_recip_application.Features.Importation.Services;
using simple_recip_application.Features.Importation.Store.Actions;
using simple_recip_application.Features.IngredientsManagement.Persistence.Repositories;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore;
using simple_recip_application.Features.NotificationsManagement.Persistence.Entites;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.Importation.Store;

public class ImportEffects
(
    ILogger<ImportEffects> _logger,
    IStringLocalizer<Messages> _messagesStringLocalizer,
    IIngredientRepository _ingredientRepository,
    ILogger<CsvImportService> _csvImportLogger
)
{
    [EffectMethod]
    public async Task HandleLoadIngredients(StartImportAction action, IDispatcher dispatcher)
    {
        try
        {
            var strategy = ImportStrategyFactory.CreateImportStrategy(action.ImportStrategy, _ingredientRepository, _csvImportLogger);
            
            var importService = new ImportService(strategy);

            var result = await importService.ExecuteImport(action.ImportModel.FilePath);

            var message = _messagesStringLocalizer["ImportFailure"];
            var type = "danger";

            if (result)
            {
                dispatcher.Dispatch(new ImportSuccessAction());

                message = _messagesStringLocalizer["ImportSuccess"];
                type = "success";
            }
            else
            {
                dispatcher.Dispatch(new ImportFailureAction());
            }

            var notification = new NotificationMessage()
            {
                Message = message,
                Type = type
            };

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors du chargement des ingrédients");

            dispatcher.Dispatch(new ImportFailureAction());

            var notification = new NotificationMessage()
            {
                Message = _messagesStringLocalizer["ImportFailure"],
                Type = "danger"
            };

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
        }
    }
}