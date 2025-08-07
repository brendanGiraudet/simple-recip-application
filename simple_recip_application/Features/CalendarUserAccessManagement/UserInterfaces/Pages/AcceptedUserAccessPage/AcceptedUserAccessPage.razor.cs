using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Factories;
using simple_recip_application.Features.CalendarUserAccessManagement.Store;
using simple_recip_application.Features.UserInfos.Store;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.CalendarUserAccessManagement.UserInterfaces.Pages.AcceptedUserAccessPage;

public partial class AcceptedUserAccessPage
{
    [Parameter] public string CalendarId { get; set; }

    [Inject] public IDispatcher Dispatcher { get; set; }
    [Inject] public ICalendarUserAccessModelFactory CalendarUserAccessModelFactory { get; set; }
    [Inject] public IState<CalendarUserAccessState> CalendarUserAccessState { get; set; }
    [Inject] public IState<UserInfosState> UserInfosState { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        var userAccess = CalendarUserAccessModelFactory.CreateCalendarUserAccessModel(UserInfosState.Value.UserInfo.Id, UserInfosState.Value.UserInfo.Email, new Guid(CalendarId));

        Dispatcher.Dispatch(new AddItemAction<ICalendarUserAccessModel>(userAccess));
    }
}
