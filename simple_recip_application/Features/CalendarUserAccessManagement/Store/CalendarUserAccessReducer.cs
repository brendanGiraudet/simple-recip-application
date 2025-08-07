using Fluxor;
using simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarUserAccessManagement.Store.Actions;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.CalendarUserAccessManagement.Store;

public class CalendarUserAccessReducer
{
    #region LoadItems
    [ReducerMethod]
    public static CalendarUserAccessState ReduceLoadItemsAction(CalendarUserAccessState state, LoadItemsAction<ICalendarUserAccessModel> action) => state with { IsLoading = true, Take = action.Take, Skip = action.Skip };

    [ReducerMethod]
    public static CalendarUserAccessState ReduceLoadItemsSuccessAction(CalendarUserAccessState state, LoadItemsSuccessAction<ICalendarUserAccessModel> action)
        => state with { IsLoading = false, Items = action.Items };

    [ReducerMethod]
    public static CalendarUserAccessState ReduceLoadItemsFailureAction(CalendarUserAccessState state, LoadItemsFailureAction<ICalendarUserAccessModel> action)
        => state with { IsLoading = false };
    #endregion LoadItems

    #region ShareCalendarAction
    [ReducerMethod]
    public static CalendarUserAccessState ReduceShareCalendarAction(CalendarUserAccessState state, ShareCalendarAction action)
        => state with { IsLoading = true };
    
    [ReducerMethod]
    public static CalendarUserAccessState ReduceShareCalendarSuccessAction(CalendarUserAccessState state, ShareCalendarSuccessAction action)
        => state with { IsLoading = false, FormModalVisibility = false };
    
    [ReducerMethod]
    public static CalendarUserAccessState ReduceShareCalendarFailureAction(CalendarUserAccessState state, ShareCalendarFailureAction action)
        => state with { IsLoading = false };
    #endregion ShareCalendarAction

    #region SetFormModalVisibilityAction
    [ReducerMethod]
    public static CalendarUserAccessState ReduceSetFormModalVisibilityAction(CalendarUserAccessState state, SetFormModalVisibilityAction<ICalendarUserAccessModel> action)
        => state with { FormModalVisibility = action.IsVisible };
    #endregion SetFormModalVisibilityAction

    #region AddCalendarUserAccess
    [ReducerMethod]
    public static CalendarUserAccessState ReduceAddItemAction(CalendarUserAccessState state, AddItemAction<ICalendarUserAccessModel> action)
        => state with { IsLoading = true };
    
    [ReducerMethod]
    public static CalendarUserAccessState ReduceAddItemFailureAction(CalendarUserAccessState state, AddItemFailureAction<ICalendarUserAccessModel> action)
        => state with { IsLoading = false };
    
    [ReducerMethod]
    public static CalendarUserAccessState ReduceAddItemSuccessAction(CalendarUserAccessState state, AddItemSuccessAction<ICalendarUserAccessModel> action)
        => state with { IsLoading = false };
    #endregion AddCalendarUserAccess
}
