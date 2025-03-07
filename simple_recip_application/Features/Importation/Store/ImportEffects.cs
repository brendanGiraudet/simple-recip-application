using Fluxor;
using simple_recip_application.Features.Importation.Services;
using simple_recip_application.Features.Importation.Store.Actions;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.IngredientsManagement.Persistence.Repositories;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Enums;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Factories;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.Importation.Store;

public class ImportEffects
(
    ILogger<ImportEffects> _logger,
    IIngredientRepository _ingredientRepository,
    ILogger<CsvImportService> _csvImportLogger,
    IIngredientFactory _ingredientFactory,
    INotificationMessageFactory _notificationMessageFactory
)
{
    [EffectMethod]
    public async Task HandleLoadIngredients(StartImportAction action, IDispatcher dispatcher)
    {
        try
        {
            var message = MessagesTranslator.ImportFailure;
            NotificationType type = NotificationType.Error;

            if (!string.IsNullOrEmpty(action.ImportModel.FilePath))
            {
                var errorNotification = _notificationMessageFactory.CreateNotificationMessage(message, type);

                dispatcher.Dispatch(new AddItemAction<INotificationMessage>(errorNotification));

                return;
            }

            var strategy = ImportStrategyFactory.CreateImportStrategy(action.ImportStrategy, _ingredientRepository, _csvImportLogger, _ingredientFactory);

            var importService = new ImportService(strategy);

            var result = await importService.ExecuteImport(action.ImportModel.FilePath!);

            if (result)
            {
                dispatcher.Dispatch(new ImportSuccessAction());

                message = MessagesTranslator.ImportSuccess;
                type = NotificationType.Success;
            }
            else
            {
                dispatcher.Dispatch(new ImportFailureAction());
            }

            var notification = _notificationMessageFactory.CreateNotificationMessage(message, type);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors du chargement des ingrédients");

            dispatcher.Dispatch(new ImportFailureAction());

            var notification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.ImportFailure, NotificationType.Error);

            dispatcher.Dispatch(new AddItemAction<INotificationMessage>(notification));
        }
    }
}