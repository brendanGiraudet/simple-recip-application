using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using simple_recip_application.Constants;
using simple_recip_application.Features.CalendarManagement.Store;
using simple_recip_application.Features.CalendarUserAccessManagement.Store;
using simple_recip_application.Features.CalendarUserAccessManagement.Store.Actions;
using simple_recip_application.Settings;

namespace simple_recip_application.Features.CalendarUserAccessManagement.UserInterfaces.Components.CalendarUserAccessForm;

public partial class CalendarUserAccessForm
{
    [Inject] public IState<CalendarUserAccessState> CalendarUserAccessState { get; set; }
    [Inject] public IState<CalendarState> CalendarState { get; set; }
    [Inject] public IOptions<SimpleRecipeApplicationSettings> SimpleRecipeApplicationSettingsOptions { get; set; }
    SimpleRecipeApplicationSettings _simpleRecipeApplicationSettings => SimpleRecipeApplicationSettingsOptions.Value;

    [Parameter] public EventCallback<bool> OnCancel { get; set; }

    private void Submit()
    {
        var url = $"{_simpleRecipeApplicationSettings.BaseUrl}{PageUrlsConstants.GetAcceptedCalendarUserAccessesPage(CalendarState.Value.Item.Id.Value)}";

        Dispatcher.Dispatch(new ShareCalendarAction(CalendarUserAccessState.Value.Item.UserEmail, CalendarState.Value.Item.Name, url));
    }

    protected async Task OnCancelAsync()
    {
        if (OnCancel.HasDelegate)
            await OnCancel.InvokeAsync(false);
    }
}
