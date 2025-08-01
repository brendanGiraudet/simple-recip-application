using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Factories;
using simple_recip_application.Features.CalendarManagement.Store;
using simple_recip_application.Features.Importation.Store;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.NotificationsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.UserInfos.Store;
using simple_recip_application.Settings;
using simple_recip_application.Store.Actions;
using System.Globalization;

namespace simple_recip_application.Features.CalendarManagement.UserInterfaces.Components.CalendarForm;

public partial class CalendarForm
{
    [Inject] public required IFeatureManager FeatureManager { get; set; }
    [Inject] public required IState<CalendarState> CalendarState { get; set; }
    [Inject] public required IState<ImportState> ImportState { get; set; }
    [Inject] public required IState<UserInfosState> UserInfosState { get; set; }
    [Inject] public required INotificationMessageFactory NotificationMessageFactory { get; set; }
    [Inject] public required ILogger<CalendarForm> Logger { get; set; }
    [Inject] public required IImportModelFactory ImportModelFactory { get; set; }
    [Inject] public required IOptions<FileSettings> FileSettingsOptions { get; set; }
    [Inject] public required ICalendarUserAccessModelFactory CalendarUserAccessModelFactory { get; set; }

    private FileSettings _fileSettings => FileSettingsOptions.Value;

    public ICalendarModel Calendar
    {
        get
        {
            return CalendarState.Value.Item;
        }
        set
        {
            Dispatcher.Dispatch(new SetItemAction<ICalendarModel>(value));
        }
    }
    [Parameter] public EventCallback<bool> OnCancel { get; set; }

    protected async Task OnCancelAsync()
    {
        if (OnCancel.HasDelegate)
            await OnCancel.InvokeAsync(false);
    }

    protected void Submit()
    {
        if (Calendar.Id.HasValue)
            Dispatcher.Dispatch(new UpdateItemAction<ICalendarModel>(Calendar));

        else
            Dispatcher.Dispatch(new AddItemAction<ICalendarModel>(Calendar));
    }

    private void DeleteCalendar(ICalendarModel model)
    {
        if (model.Id.HasValue)
            Dispatcher.Dispatch(new DeleteItemAction<ICalendarModel>(model));
    }

    private string GetDeleteButtonCssClass() => Calendar.Id.HasValue ? "" : "hidden";
}