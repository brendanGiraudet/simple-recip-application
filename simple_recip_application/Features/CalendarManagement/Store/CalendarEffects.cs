using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Constants;
using simple_recip_application.Dtos;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Repositories;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.CalendarManagement.Store;

public class CalendarEffects
(
    ILogger<CalendarEffects> _logger,
    NavigationManager _navigationManager,
    IServiceScopeFactory _scopeFactory
)
{
    [EffectMethod]
    public async Task HandleLoadCalendars(LoadItemsAction<ICalendarModel> action, IDispatcher dispatcher)
    {
        try
        {
            await Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<ICalendarRepository>();

                var recipesResult = await repository.GetAsync(action.Take, action.Skip, action.Predicate, action.Sort);

                if (!recipesResult.Success)
                    dispatcher.Dispatch(new LoadItemsFailureAction<ICalendarModel>());

                else
                    dispatcher.Dispatch(new LoadItemsSuccessAction<ICalendarModel>(recipesResult.Item!));
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors du chargement des calendriers");

            dispatcher.Dispatch(new LoadItemsFailureAction<ICalendarModel>());
        }
    }

    [EffectMethod]
    public async Task HandleLoadCalendar(LoadItemAction<ICalendarModel> action, IDispatcher dispatcher)
    {
        try
        {
            await Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<ICalendarRepository>();

                var recipeResult = await repository.GetByIdAsync(action.Id);

                if (!recipeResult.Success)
                    dispatcher.Dispatch(new LoadItemFailureAction<ICalendarModel>());

                else
                    dispatcher.Dispatch(new LoadItemSuccessAction<ICalendarModel>(recipeResult.Item));
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors du chargement du calendrier");

            dispatcher.Dispatch(new LoadItemFailureAction<ICalendarModel>());
        }
    }

    [EffectMethod]
    public async Task HandleAddCalendar(AddItemAction<ICalendarModel> action, IDispatcher dispatcher)
    {
        try
        {
            await Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();

                var repository = scope.ServiceProvider.GetRequiredService<ICalendarRepository>();

                var addResult = await repository.AddAsync(action.Item);

                if (!addResult.Success)
                    dispatcher.Dispatch(new AddItemFailureAction<ICalendarModel>(action.Item));

                else
                {
                    dispatcher.Dispatch(new AddItemSuccessAction<ICalendarModel>(action.Item));
                    dispatcher.Dispatch(new SetFormModalVisibilityAction<ICalendarModel>(false));
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de l'ajout d'un calendrier");

            dispatcher.Dispatch(new AddItemFailureAction<ICalendarModel>(action.Item));
        }
    }

    [EffectMethod]
    public async Task HandleDeleteCalendar(DeleteItemSuccessAction<ICalendarModel> action, IDispatcher dispatcher)
    {
        _navigationManager.NavigateTo(PageUrlsConstants.CalendarsPage);
    }

    [EffectMethod]
    public async Task HandleDeleteCalendar(DeleteItemAction<ICalendarModel> action, IDispatcher dispatcher)
    {
        var deleteResult = await DeleteCalendar(action.Item);

        if (!deleteResult.Success)
            dispatcher.Dispatch(new DeleteItemFailureAction<ICalendarModel>(action.Item));

        else
        {
            dispatcher.Dispatch(new DeleteItemSuccessAction<ICalendarModel>(action.Item));
            dispatcher.Dispatch(new SetFormModalVisibilityAction<ICalendarModel>(false));
        }
    }

    private async Task<MethodResult> DeleteCalendar(ICalendarModel recipe)
    {
        try
        {
            return await Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<ICalendarRepository>();

                if (!recipe.Id.HasValue)
                    return new MethodResult(false);

                var recipeResult = await repository.GetByIdAsync(recipe.Id.Value);
                if (!recipeResult.Success || recipeResult.Item == null)
                    return new MethodResult(false);

                var deleteResult = await repository.DeleteAsync(recipeResult.Item);

                return new MethodResult(deleteResult.Success);
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la suppression du calendrier");

            return new MethodResult(false);
        }
    }

    [EffectMethod]
    public async Task HandleDeleteCalendars(DeleteItemsAction<ICalendarModel> action, IDispatcher dispatcher)
    {
        var deleteSuccess = true;
        foreach (var recipe in action.Items)
        {
            var deleteResult = await DeleteCalendar(recipe);

            deleteSuccess &= deleteResult.Success;
        }

        if (!deleteSuccess)
            dispatcher.Dispatch(new DeleteItemsFailureAction<ICalendarModel>(action.Items));
        else
        {
            dispatcher.Dispatch(new DeleteItemsSuccessAction<ICalendarModel>(action.Items));
        }
    }

    [EffectMethod]
    public async Task HandleUpdateCalendar(UpdateItemAction<ICalendarModel> action, IDispatcher dispatcher)
    {
        try
        {
            await Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<ICalendarRepository>();

                var updateResult = await repository.UpdateAsync(action.Item);

                if (!updateResult.Success)
                    dispatcher.Dispatch(new UpdateItemFailureAction<ICalendarModel>(action.Item));

                else
                {
                    dispatcher.Dispatch(new UpdateItemSuccessAction<ICalendarModel>(action.Item));
                    dispatcher.Dispatch(new SetFormModalVisibilityAction<ICalendarModel>(false));
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la mise à jour");

            dispatcher.Dispatch(new UpdateItemFailureAction<ICalendarModel>(action.Item));
        }
    }
}
