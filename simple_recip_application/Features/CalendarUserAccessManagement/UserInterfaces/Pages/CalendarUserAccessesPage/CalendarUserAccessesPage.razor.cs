using Fluxor;
using Microsoft.AspNetCore.Components;
using simple_recip_application.Components.OptionsMenu;
using simple_recip_application.Constants;
using simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Factories;
using simple_recip_application.Features.CalendarUserAccessManagement.Store;
using simple_recip_application.Features.UserInfos.Store;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;
using System.Linq.Expressions;

namespace simple_recip_application.Features.CalendarUserAccessManagement.UserInterfaces.Pages.CalendarUserAccessesPage;

public partial class CalendarUserAccessesPage
{
    [Inject] public required IState<UserInfosState> UserInfosState { get; set; }
    [Inject] public required IState<CalendarUserAccessState> CalendarUserAccessState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required ICalendarUserAccessModelFactory CalendarUserAccessModelFactory { get; set; }

    [Parameter] public required string CalendarId { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        LoadFilteredCalendarUserAccesses();
    }

    private string _searchTerm = string.Empty;

    private void LoadFilteredCalendarUserAccesses(int? skip = null)
    {
        Expression<Func<ICalendarUserAccessModel, bool>>? filter = r => r.CalendarId.ToString().ToLower() == CalendarId.ToLower();

        if (!string.IsNullOrEmpty(_searchTerm))
            filter = i => i.UserEmail.ToLower().Contains(_searchTerm.ToLower());

        Dispatcher.Dispatch(new LoadItemsAction<ICalendarUserAccessModel>(Take: CalendarUserAccessState.Value.Take, Skip: skip ?? 0, filter));
    }

    private void OnSearchTermChanged(string searchTerm)
    {
        _searchTerm = searchTerm;
        LoadFilteredCalendarUserAccesses();
    }

    private string GetCalendarUserAccessesVisibilityCssClass() => !CalendarUserAccessState.Value.IsLoading ? "" : "hidden";

    private async Task DeleteCalendarUserAccessAsync(ICalendarUserAccessModel calendarUserAccess)
    {
        Dispatcher.Dispatch(new DeleteItemAction<ICalendarUserAccessModel>(calendarUserAccess));

        await Task.CompletedTask;
    }

    private bool CanPreviousClick() => CalendarUserAccessState.Value.Skip > 0;
    private async Task OnPreviousAsync()
    {
        if (!CanPreviousClick()) return;

        var skip = CalendarUserAccessState.Value.Skip - CalendarUserAccessState.Value.Take;

        skip = skip < 0 ? 0 : skip;

        LoadFilteredCalendarUserAccesses(skip);

        await Task.CompletedTask;
    }

    private async Task OnNextAsync()
    {
        var skip = CalendarUserAccessState.Value.Skip + CalendarUserAccessState.Value.Take;

        LoadFilteredCalendarUserAccesses(skip);

        await Task.CompletedTask;
    }

    private List<OptionMenuItem> GetOptions()
    {
        List<OptionMenuItem> options = [];

        options.Add(new(MaterialIconsConstants.Add, string.Empty, () => OpenCalendarUserAccessFormModalAsync(), LabelsTranslator.AddCalendar));

        return options;
    }

    private async Task OpenCalendarUserAccessFormModalAsync(ICalendarUserAccessModel? model = null)
    {
        Dispatcher.Dispatch(new SetFormModalVisibilityAction<ICalendarUserAccessModel>(true));

        await Task.CompletedTask;
    }

    private void CloseCalendarUserAccessFormModal(bool isUpdated) => Dispatcher.Dispatch(new SetFormModalVisibilityAction<ICalendarUserAccessModel>(false));
}
