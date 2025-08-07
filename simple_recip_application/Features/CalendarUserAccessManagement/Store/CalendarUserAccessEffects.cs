using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using simple_recip_application.Constants;
using simple_recip_application.Emails.Templates.AddCalendarUserAccessTemplate;
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
    IEmailService _emailService,
    IServiceProvider provider,
    NavigationManager _navigationManager
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
    public async Task HandleAddItemFailureAction(AddItemFailureAction<ICalendarUserAccessModel> action, IDispatcher dispatcher)
    {
        _navigationManager.NavigateTo(PageUrlsConstants.GetCalendarUserAccessesPage(action.Item.CalendarId));
    }
    
    
    [EffectMethod]
    public async Task HandleAddItemSuccessAction(AddItemSuccessAction<ICalendarUserAccessModel> action, IDispatcher dispatcher)
    {
        _navigationManager.NavigateTo(PageUrlsConstants.GetCalendarUserAccessesPage(action.Item.CalendarId));
    }
    
    [EffectMethod]
    public async Task HandleDeleteItemAction(DeleteItemAction<ICalendarUserAccessModel> action, IDispatcher dispatcher)
    {
        try
        {
            await Task.Run(async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var repository = scope.ServiceProvider.GetRequiredService<ICalendarUserAccessRepository>();

                var result = await repository.DeleteAsync(action.Item);

                if (!result.Success)
                    dispatcher.Dispatch(new DeleteItemFailureAction<ICalendarUserAccessModel>(action.Item));

                else
                    dispatcher.Dispatch(new DeleteItemSuccessAction<ICalendarUserAccessModel>(action.Item));

            }).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de la suppression d'un access utilisateur {action.Item.UserEmail} au calendrier {action.Item.CalendarModel?.Name}");

            dispatcher.Dispatch(new DeleteItemFailureAction<ICalendarUserAccessModel>(action.Item));
        }
    }


    [EffectMethod]
    public async Task HandleShareCalendarAction(ShareCalendarAction action, IDispatcher dispatcher)
    {
        try
        {
            var renderer = provider.GetRequiredService<HtmlRenderer>();
            var parameters = ParameterView.FromDictionary(new Dictionary<string, object> {
                { nameof(AddCalendarUserAccessTemplate.AcceptanceUrl), action.AcceptanceUrl },
                { nameof(AddCalendarUserAccessTemplate.CalendarName), action.CalendarName },
                { nameof(AddCalendarUserAccessTemplate.EmailSender), action.UserEmail }
            });

            await renderer.Dispatcher.InvokeAsync(async () =>
            {
                var output = await renderer.RenderComponentAsync<AddCalendarUserAccessTemplate>(parameters);
                var html = output.ToHtmlString();

                var result = await _emailService.SendEmailAsync(action.UserEmail, html);

                dispatcher.Dispatch(result.Success
                                    ? new ShareCalendarSuccessAction()
                                    : new ShareCalendarFailureAction());
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de l'envoie par mail l'ajout d'un access utilisateur {action.UserEmail} au calendrier {action.CalendarName}");

            dispatcher.Dispatch(new ShareCalendarFailureAction());
        }
    }
}
