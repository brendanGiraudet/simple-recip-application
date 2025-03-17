using Fluxor;
using simple_recip_application.Features.Importation.Services;
using simple_recip_application.Features.Importation.Store.Actions;
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
    IServiceProvider _serviceProvider,
    IIngredientRepository _ingredientRepository
)
{
    [EffectMethod]
    public async Task HandleLoadIngredients(StartImportAction action, IDispatcher dispatcher)
    {
        try
        {
            if (action.ImportModel.FileContent?.Length == 0)
            {
                var errorNotification = _notificationMessageFactory.CreateNotificationMessage(MessagesTranslator.ImportFailure, NotificationType.Error);

                dispatcher.Dispatch(new AddItemAction<INotificationMessage>(errorNotification));

                dispatcher.Dispatch(new ImportFailureAction());

                return;
            }

            var strategy = ImportStrategyFactory.CreateImportStrategy(action.ImportStrategy, _serviceProvider, dispatcher, _ingredientRepository);

            var importService = new ImportService(strategy);

            var result = await importService.ExecuteImport(action.ImportModel.FileContent!);

            if (result.Success)
                dispatcher.Dispatch(new ImportSuccessAction());

            else
                dispatcher.Dispatch(new ImportFailureAction());

            var notification = _notificationMessageFactory.CreateNotificationMessage(result.Message, result.Success ? NotificationType.Success : NotificationType.Error);

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