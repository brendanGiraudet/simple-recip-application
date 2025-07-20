using Fluxor;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.EqualityComparers;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.CalendarManagement.Store;

public static class CalendarReducer
{
    #region LoadItems
    [ReducerMethod]
    public static CalendarState ReduceLoadItemsAction(CalendarState state, LoadItemsAction<ICalendarModel> action) => state with { IsLoading = true, Take = action.Take, Skip = action.Skip };

    [ReducerMethod]
    public static CalendarState ReduceLoadItemsSuccessAction(CalendarState state, LoadItemsSuccessAction<ICalendarModel> action)
        => state with { IsLoading = false, Items = action.Items };

    [ReducerMethod]
    public static CalendarState ReduceLoadItemsFailureAction(CalendarState state, LoadItemsFailureAction<ICalendarModel> action)
        => state with { IsLoading = false };
    #endregion

    #region LoadItem
    [ReducerMethod]
    public static CalendarState ReduceLoadItemAction(CalendarState state, LoadItemAction<ICalendarModel> action) => state with { IsLoading = true };

    [ReducerMethod]
    public static CalendarState ReduceLoadItemSuccessAction(CalendarState state, LoadItemSuccessAction<ICalendarModel> action)
        => state with { IsLoading = false, Item = action.Item };

    [ReducerMethod]
    public static CalendarState ReduceLoadItemFailureAction(CalendarState state, LoadItemFailureAction<ICalendarModel> action)
        => state with { IsLoading = false };
    #endregion

    #region AddCalendar
    [ReducerMethod]
    public static CalendarState ReduceAddItemAction(CalendarState state, AddItemAction<ICalendarModel> action) =>
        state with { Items = state.Items, IsLoading = true };

    [ReducerMethod]
    public static CalendarState ReduceAddItemSuccessAction(CalendarState state, AddItemSuccessAction<ICalendarModel> action) =>
        state with { Items = [.. state.Items, action.Item], IsLoading = false };

    [ReducerMethod]
    public static CalendarState ReduceAddItemFailureAction(CalendarState state, AddItemFailureAction<ICalendarModel> action) =>
        state with { Items = state.Items, IsLoading = false };
    #endregion

    #region DeleteCalendar
    [ReducerMethod]
    public static CalendarState ReduceDeleteItemAction(CalendarState state, DeleteItemAction<ICalendarModel> action) => state with { IsLoading = true };

    [ReducerMethod]
    public static CalendarState ReduceDeleteItemSuccessAction(CalendarState state, DeleteItemSuccessAction<ICalendarModel> action)
    {
        var items = state.Items.Where(i => !new CalendarEqualityComparer().Equals(i, action.Item)).ToList();

        return state with { Items = items, IsLoading = false };
    }

    [ReducerMethod]
    public static CalendarState ReduceDeleteItemFailureAction(CalendarState state, DeleteItemFailureAction<ICalendarModel> action) => state with { IsLoading = false };
    #endregion

    #region UpdateCalendar
    [ReducerMethod]
    public static CalendarState ReduceUpdateItemAction(CalendarState state, UpdateItemAction<ICalendarModel> action) =>
        state with { IsLoading = true };

    [ReducerMethod]
    public static CalendarState ReduceUpdateItemSuccessAction(CalendarState state, UpdateItemSuccessAction<ICalendarModel> action)
    {
        var items = state.Items.Select(i => i.Id == action.Item.Id ? action.Item : i).ToList();
        return state with { Items = items, IsLoading = false, Item = action.Item };
    }

    [ReducerMethod]
    public static CalendarState ReduceUpdateItemFailureAction(CalendarState state, UpdateItemFailureAction<ICalendarModel> action) =>
        state with { IsLoading = false };
    #endregion

    #region SetCalendarFormModalVisibility
    [ReducerMethod]
    public static CalendarState ReduceSetCalendarFormModalVisibilityAction(CalendarState state, SetFormModalVisibilityAction<ICalendarModel> action) =>
        state with { FormModalVisibility = action.IsVisible };
    #endregion

    #region SetItem
    [ReducerMethod]
    public static CalendarState ReduceSetItemAction(CalendarState state, SetItemAction<ICalendarModel> action) =>
        state with
        {
            Item = action.Item,
        };
    #endregion

    #region SetLoadingAction
    [ReducerMethod]
    public static CalendarState ReduceSetLoadingAction(CalendarState state, SetLoadingAction<ICalendarModel> action) => state with { IsLoading = action.IsLoading };
    #endregion SetLoadingAction
}