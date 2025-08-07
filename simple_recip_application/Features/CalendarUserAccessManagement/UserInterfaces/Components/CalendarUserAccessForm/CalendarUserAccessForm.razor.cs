using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Features.CalendarUserAccessManagement.Store;
using simple_recip_application.Features.CalendarUserAccessManagement.Store.Actions;

namespace simple_recip_application.Features.CalendarUserAccessManagement.UserInterfaces.Components.CalendarUserAccessForm;

public partial class CalendarUserAccessForm
{
    [Inject] public IState<CalendarUserAccessState> CalendarUserAccessState { get; set; }

    [Parameter] public EventCallback<bool> OnCancel { get; set; }

    private void Submit()
    {
        Dispatcher.Dispatch(new ShareCalendarAction(CalendarUserAccessState.Value.Item));
    }

    protected async Task OnCancelAsync()
    {
        if (OnCancel.HasDelegate)
            await OnCancel.InvokeAsync(false);
    }
}
