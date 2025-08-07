using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using simple_recip_application.Features.CalendarManagement.Store;
using simple_recip_application.Features.CalendarUserAccessManagement.Store;
using simple_recip_application.Features.CalendarUserAccessManagement.Store.Actions;
using simple_recip_application.Settings;

namespace simple_recip_application.Features.CalendarUserAccessManagement.UserInterfaces.Components.CalendarUserAccessForm;

public partial class CalendarUserAccessForm
{
    [Inject] public IState<CalendarUserAccessState> CalendarUserAccessState { get; set; }
    [Inject] public IState<CalendarState> CalendarState { get; set; }
    [Inject] public IOptions<EmailsSettings> EmailsSettingsOptions { get; set; }
    EmailsSettings _emailsSettings => EmailsSettingsOptions.Value;

    [Parameter] public EventCallback<bool> OnCancel { get; set; }

    private void Submit()
    {
        Dispatcher.Dispatch(new ShareCalendarAction(CalendarUserAccessState.Value.Item.UserEmail, CalendarState.Value.Item.Name, _emailsSettings.AddCalendarUserAccessTemplateAcceptanceUrl));
    }

    protected async Task OnCancelAsync()
    {
        if (OnCancel.HasDelegate)
            await OnCancel.InvokeAsync(false);
    }
}
