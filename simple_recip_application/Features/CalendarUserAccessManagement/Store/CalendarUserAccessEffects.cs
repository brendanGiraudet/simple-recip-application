using Fluxor;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.CalendarManagement.Store;
using simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarUserAccessManagement.Store.Actions;
using simple_recip_application.Services.EmailService;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.CalendarUserAccessManagement.Store;

public class CalendarUserAccessEffects
(
    ILogger<CalendarEffects> _logger,
    IServiceScopeFactory _scopeFactory,
    IEmailService _emailService
)
{
    [EffectMethod]
    public async Task HandleLoadCalendars(LoadItemsAction<ICalendarUserAccessModel> action, IDispatcher dispatcher)
    {
        try
        {
            await Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<ICalendarUserAccessRepository>();

                var recipesResult = await repository.GetAsync(action.Take, action.Skip, action.Predicate, action.Sort);

                if (!recipesResult.Success)
                    dispatcher.Dispatch(new LoadItemsFailureAction<ICalendarUserAccessModel>());

                else
                    dispatcher.Dispatch(new LoadItemsSuccessAction<ICalendarUserAccessModel>(recipesResult.Item!));

            }).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors du chargement des calendriers");

            dispatcher.Dispatch(new LoadItemsFailureAction<ICalendarUserAccessModel>());
        }
    }

    [EffectMethod]
    public async Task HandleAddItemAction(AddItemAction<ICalendarUserAccessModel> action, IDispatcher dispatcher)
    {
        try
        {
            await Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<ICalendarUserAccessRepository>();

                var result = await repository.AddAsync(action.Item);

                if (!result.Success)
                    dispatcher.Dispatch(new AddItemFailureAction<ICalendarUserAccessModel>(action.Item));

                else
                    dispatcher.Dispatch(new AddItemSuccessAction<ICalendarUserAccessModel>(action.Item));

            }).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de l'ajout d'un access utilisateur {action.Item.UserEmail} au calendrier {action.Item.CalendarModel?.Name}");

            dispatcher.Dispatch(new AddItemFailureAction<ICalendarUserAccessModel>(action.Item));
        }
    }
    
    
    [EffectMethod]
    public async Task HandleShareCalendarAction(ShareCalendarAction action, IDispatcher dispatcher)
    {
        try
        {
            await Task.Run(async () =>
            {
                // TODO faire le message 
                var result = await _emailService.SendEmailAsync(action.CalendarUserAccessModel.UserEmail, "test");

                if (!result.Success)
                    dispatcher.Dispatch(new ShareCalendarFailureAction());

                else
                    dispatcher.Dispatch(new ShareCalendarSuccessAction());

            }).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de l'envoie par mail l'ajout d'un access utilisateur {action.CalendarUserAccessModel.UserEmail} au calendrier {action.CalendarUserAccessModel.CalendarModel?.Name}");

            dispatcher.Dispatch(new ShareCalendarFailureAction());
        }
    }
}
