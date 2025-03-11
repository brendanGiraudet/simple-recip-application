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
    INotificationMessageFactory _notificationMessageFactory,
    IServiceProvider _serviceProvider
)
{
    [EffectMethod]
    public async Task HandleLoadIngredients(StartImportAction action, IDispatcher dispatcher)
    {
        try
        {
            var message = MessagesTranslator.ImportFailure;
            NotificationType type = NotificationType.Error;

            if (action.ImportModel.FileContent?.Length == 0)
            {
                var errorNotification = _notificationMessageFactory.CreateNotificationMessage(message, type);

                dispatcher.Dispatch(new AddItemAction<INotificationMessage>(errorNotification));

                return;
            }

            var strategy = ImportStrategyFactory.CreateImportStrategy(action.ImportStrategy, _serviceProvider);

            var importService = new ImportService(strategy);

            var result = await importService.ExecuteImport(action.ImportModel.FileContent!);

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