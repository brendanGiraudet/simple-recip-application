using Fluxor;
using simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Entities;
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
    #endregion
}
