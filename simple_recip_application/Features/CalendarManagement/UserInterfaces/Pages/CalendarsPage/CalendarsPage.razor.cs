using Fluxor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using simple_recip_application.Components.OptionsMenu;
using simple_recip_application.Constants;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Factories;
using simple_recip_application.Features.CalendarManagement.Store;
using simple_recip_application.Features.UserInfos.Store;
using simple_recip_application.Resources;
using simple_recip_application.Store.Actions;
using System.Linq.Expressions;

namespace simple_recip_application.Features.CalendarManagement.UserInterfaces.Pages.CalendarsPage;

public partial class CalendarsPage
{
    [Inject] public required IState<UserInfosState> UserInfosState { get; set; }
    [Inject] public required IState<CalendarState> CalendarState { get; set; }
    [Inject] public required IDispatcher Dispatcher { get; set; }
    [Inject] public required ICalendarModelFactory CalendarFactory { get; set; }
    [Inject] public required NavigationManager NavigationManager { get; set; }
    [Inject] public required IAuthorizationService AuthorizationService { get; set; }
    [Inject] public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private bool _canManageCalendar;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

        var authorizationResult = await AuthorizationService.AuthorizeAsync(authenticationState.User, FeatureFlagsConstants.CalendarManagementFeature);

        _canManageCalendar = authorizationResult.Succeeded;
    }

    private async Task OpenCalendarFormModalAsync(ICalendarModel? model = null)
    {
        if (!_canManageCalendar) return;

        if (model is not null && model.Id.HasValue)
            Dispatcher.Dispatch(new LoadItemAction<ICalendarModel>(model.Id.Value));

        else
            Dispatcher.Dispatch(new SetItemAction<ICalendarModel>(CalendarFactory.CreateCalendarModel()));

        Dispatcher.Dispatch(new SetFormModalVisibilityAction<ICalendarModel>(true));

        await Task.CompletedTask;
    }

    private void CloseCalendarFormModal(bool isUpdated) => Dispatcher.Dispatch(new SetFormModalVisibilityAction<ICalendarModel>(false));

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (CalendarState.Value.Items.Count() == 0)
            LoadFilteredCalendars();
    }

    private string GetCalendarsVisibilityCssClass() => !CalendarState.Value.IsLoading ? "" : "hidden";

    private List<OptionMenuItem> GetOptions()
    {
        List<OptionMenuItem> options = [];

        if (_canManageCalendar)
            options.Add(new(MaterialIconsConstants.Add, string.Empty, () => OpenCalendarFormModalAsync(), LabelsTranslator.AddCalendar));

        return options;
    }

    private async Task DeleteCalendarAsync(ICalendarModel calendarModel)
    {
        Dispatcher.Dispatch(new DeleteItemAction<ICalendarModel>(calendarModel));

        await Task.CompletedTask;
    }

    private string _searchTerm = string.Empty;

    private void OnSearchTermChanged(string searchTerm)
    {
        _searchTerm = searchTerm;
        LoadFilteredCalendars();
    }

    private void LoadFilteredCalendars(int? skip = null)
    {
        Expression<Func<ICalendarModel, bool>>? filter = r => r.RemoveDate == null && r.CalendarUserAccessModels.Any(a => a.UserId == UserInfosState.Value.UserInfo.Id);

        if (!string.IsNullOrEmpty(_searchTerm))
            filter = i => i.Name.ToLower().Contains(_searchTerm.ToLower()) && i.RemoveDate == null;

        Dispatcher.Dispatch(new LoadItemsAction<ICalendarModel>(Take: CalendarState.Value.Take, Skip: skip ?? 0, filter));
    }

    private bool CanPreviousClick() => CalendarState.Value.Skip > 0;
    private async Task OnPreviousAsync()
    {
        if (!CanPreviousClick()) return;

        var skip = CalendarState.Value.Skip - CalendarState.Value.Take;

        skip = skip < 0 ? 0 : skip;

        LoadFilteredCalendars(skip);

        await Task.CompletedTask;
    }

    private async Task OnNextAsync()
    {
        var skip = CalendarState.Value.Skip + CalendarState.Value.Take;

        LoadFilteredCalendars(skip);

        await Task.CompletedTask;
    }
}