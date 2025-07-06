using Fluxor;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.EqualityComparers;
using simple_recip_application.Features.RecipePlanningFeature.Store.Actions;
using simple_recip_application.Features.ShoppingListManagement.Store.Actions;
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

    #region DeleteItem
    [ReducerMethod]
    public static PlanifiedRecipeState ReduceDeleteItemSuccessAction(PlanifiedRecipeState state, DeleteItemSuccessAction<IPlanifiedRecipeModel> action)
    {
        var items = state.Items.Where(c => !new PlanifiedRecipeEqualityComparer().Equals(c, action.Item));

        return state with { IsLoading = false, Items = items, RecipesGroupedByDay = GroupRecipesByDay(items) };
    }
    #endregion

    #region SetCurrentWeekStart
    [ReducerMethod]
    public static PlanifiedRecipeState ReduceSetCurrentWeekStartAction(PlanifiedRecipeState state, SetCurrentWeekStartAction action)
        => state with { CurrentWeekStart = action.CurrentWeekStart };
    #endregion

    #region  PlanifiedRecipesForTheWeek
    [ReducerMethod]
    public static PlanifiedRecipeState ReducePlanifiedRecipesForTheWeekAction(PlanifiedRecipeState state, PlanifiedRecipesForTheWeekAction action)
        => state with { IsLoading = true };

    [ReducerMethod]
    public static PlanifiedRecipeState ReducePlanifiedRecipesForTheWeekSuccessAction(PlanifiedRecipeState state, PlanifiedRecipesForTheWeekSuccessAction action)
        => state with { IsLoading = false, RecipesGroupedByDay = action.PlanifiedRecipes };

    [ReducerMethod]
    public static PlanifiedRecipeState ReducePlanifiedRecipesForTheWeekFailureAction(PlanifiedRecipeState state, PlanifiedRecipesForTheWeekFailureAction action)
        => state with { IsLoading = false };
    #endregion

    #region  PlanifiedRecipeAutomaticaly
    [ReducerMethod]
    public static PlanifiedRecipeState ReducePlanifiedRecipeAutomaticalyAction(PlanifiedRecipeState state, PlanifiedRecipeAutomaticalyAction action)
        => state with { IsLoading = true };

    [ReducerMethod]
    public static PlanifiedRecipeState ReducePlanifiedRecipeAutomaticalySuccessAction(PlanifiedRecipeState state, PlanifiedRecipeAutomaticalySuccessAction action) 
        => state with { IsLoading = false };

    [ReducerMethod]
    public static PlanifiedRecipeState ReducePlanifiedRecipeAutomaticalyFailureAction(PlanifiedRecipeState state, PlanifiedRecipeAutomaticalyFailureAction action)
        => state with { IsLoading = false };
    #endregion

    #region GenerateShoppingList
    [ReducerMethod]
    public static PlanifiedRecipeState ReduceGenerateShoppingListAction(PlanifiedRecipeState state, GenerateShoppingListAction action)
        => state with { IsLoading = false };
    
    [ReducerMethod]
    public static PlanifiedRecipeState ReduceGenerateShoppingListFailureAction(PlanifiedRecipeState state, GenerateShoppingListFailureAction action)
        => state with { IsLoading = false };
    
    [ReducerMethod]
    public static PlanifiedRecipeState ReduceGenerateShoppingListSuccessAction(PlanifiedRecipeState state, GenerateShoppingListSuccessAction action)
        => state with { IsLoading = false };
    #endregion
}