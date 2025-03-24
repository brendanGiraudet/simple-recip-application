using Fluxor;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipePlanningFeature.Store.Actions;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipePlanningFeature.Store;

public static class PlanifiedRecipeReducer
{
    #region LoadItems
    [ReducerMethod]
    public static PlanifiedRecipeState ReduceLoadItemsAction(PlanifiedRecipeState state, LoadItemsAction<IPlanifiedRecipeModel> action) => state with { IsLoading = true, Take = action.Take, Skip = action.Skip };

    [ReducerMethod]
    public static PlanifiedRecipeState ReduceLoadItemsSuccessAction(PlanifiedRecipeState state, LoadItemsSuccessAction<IPlanifiedRecipeModel> action)
    {
        return state with { IsLoading = false, Items = action.Items, RecipesGroupedByDay = GroupRecipesByDay(action.Items) };
    }
    private static Dictionary<DayOfWeek, List<IPlanifiedRecipeModel>> GroupRecipesByDay(IEnumerable<IPlanifiedRecipeModel> planifiedRecipeModels)
    {
        var recipesByDay = Enum.GetValues<DayOfWeek>()
            .OrderBy(d => d == DayOfWeek.Sunday ? 7 : (int)d)
            .ToDictionary(day => day, day => new List<IPlanifiedRecipeModel>());

        foreach (var recipe in planifiedRecipeModels)
        {
            recipesByDay[recipe.PlanifiedDateTime.DayOfWeek].Add(recipe);
        }

        return recipesByDay;
    }

    [ReducerMethod]
    public static PlanifiedRecipeState ReduceLoadItemsFailureAction(PlanifiedRecipeState state, LoadItemsFailureAction<IPlanifiedRecipeModel> action)
        => state with { IsLoading = false };
    #endregion

    #region AddItem
    [ReducerMethod]
    public static PlanifiedRecipeState ReduceAddItemAction(PlanifiedRecipeState state, AddItemAction<IPlanifiedRecipeModel> action) => state with { IsLoading = true };

    [ReducerMethod]
    public static PlanifiedRecipeState ReduceAddItemSuccessAction(PlanifiedRecipeState state, AddItemSuccessAction<IPlanifiedRecipeModel> action)
        => state with { IsLoading = false, Items = [.. state.Items, action.Item], RecipesGroupedByDay = GroupRecipesByDay([.. state.Items, action.Item]) };

    [ReducerMethod]
    public static PlanifiedRecipeState ReduceAddItemFailureAction(PlanifiedRecipeState state, AddItemFailureAction<IPlanifiedRecipeModel> action)
        => state with { IsLoading = false };
    #endregion

    #region SetCurrentWeekStart
    [ReducerMethod]
    public static PlanifiedRecipeState ReduceSetCurrentWeekStartAction(PlanifiedRecipeState state, SetCurrentWeekStartAction action)
        => state with { CurrentWeekStart = action.CurrentWeekStart };
    #endregion
}