using Fluxor;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Repositories;
using simple_recip_application.Features.CalendarManagement.Store;
using simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Entities;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.CalendarUserAccessManagement.Store;

public class CalendarUserAccessEffects
(
    ILogger<CalendarEffects> _logger,
    IServiceScopeFactory _scopeFactory
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
}
