using Fluxor;
using simple_recip_application.Features.IngredientsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.EqualityComparers;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Entities;
using simple_recip_application.Store.Actions;

namespace simple_recip_application.Features.RecipesManagement.Store;

public static class RecipeReducer
{
    #region LoadItems
    [ReducerMethod]
    public static RecipeState ReduceLoadItemsAction(RecipeState state, LoadItemsAction<IRecipeModel> action) => state with { IsLoading = true, Take = action.Take, Skip = action.Skip };

    [ReducerMethod]
    public static RecipeState ReduceLoadItemsSuccessAction(RecipeState state, LoadItemsSuccessAction<IRecipeModel> action)
        => state with { IsLoading = false, Items = action.Items };

    [ReducerMethod]
    public static RecipeState ReduceLoadItemsFailureAction(RecipeState state, LoadItemsFailureAction<IRecipeModel> action)
        => state with { IsLoading = false };
    #endregion

    #region LoadItem
    [ReducerMethod]
    public static RecipeState ReduceLoadItemAction(RecipeState state, LoadItemAction<IRecipeModel> action) => state with { IsLoading = true };

    [ReducerMethod]
    public static RecipeState ReduceLoadItemSuccessAction(RecipeState state, LoadItemSuccessAction<IRecipeModel> action)
        => state with { IsLoading = false, Item = action.Item };

    [ReducerMethod]
    public static RecipeState ReduceLoadItemFailureAction(RecipeState state, LoadItemFailureAction<IRecipeModel> action)
        => state with { IsLoading = false };
    #endregion

    #region AddRecipe
    [ReducerMethod]
    public static RecipeState ReduceAddItemAction(RecipeState state, AddItemAction<IRecipeModel> action) =>
        state with { Items = state.Items, IsLoading = true };

    [ReducerMethod]
    public static RecipeState ReduceAddItemSuccessAction(RecipeState state, AddItemSuccessAction<IRecipeModel> action) =>
        state with { Items = [.. state.Items, action.Item], IsLoading = false };

    [ReducerMethod]
    public static RecipeState ReduceAddItemFailureAction(RecipeState state, AddItemFailureAction<IRecipeModel> action) =>
        state with { Items = state.Items, IsLoading = false };
    #endregion

    #region DeleteRecipe
    [ReducerMethod]
    public static RecipeState ReduceDeleteItemAction(RecipeState state, DeleteItemAction<IRecipeModel> action) => state with { IsLoading = true };

    [ReducerMethod]
    public static RecipeState ReduceDeleteItemSuccessAction(RecipeState state, DeleteItemSuccessAction<IRecipeModel> action)
    {
        var recipes = state.Items.Where(i => !new RecipeEqualityComparer().Equals(i, action.Item)).ToList();

        return state with { Items = recipes, IsLoading = false };
    }

    [ReducerMethod]
    public static RecipeState ReduceDeleteItemFailureAction(RecipeState state, DeleteItemFailureAction<IRecipeModel> action) => state with { IsLoading = false };
    #endregion

    #region UpdateRecipe
    [ReducerMethod]
    public static RecipeState ReduceUpdateItemAction(RecipeState state, UpdateItemAction<IRecipeModel> action) =>
        state with { IsLoading = true };

    [ReducerMethod]
    public static RecipeState ReduceUpdateItemSuccessAction(RecipeState state, UpdateItemSuccessAction<IRecipeModel> action)
    {
        var recipes = state.Items.Select(i => i.Id == action.Item.Id ? action.Item : i).ToList();
        return state with { Items = recipes, IsLoading = false, Item = action.Item };
    }

    [ReducerMethod]
    public static RecipeState ReduceUpdateItemFailureAction(RecipeState state, UpdateItemFailureAction<IRecipeModel> action) =>
        state with { IsLoading = false };
    #endregion

    #region SetRecipeFormModalVisibility
    [ReducerMethod]
    public static RecipeState ReduceSetRecipeFormModalVisibilityAction(RecipeState state, SetFormModalVisibilityAction<IRecipeModel> action) =>
        state with { FormModalVisibility = action.IsVisible };
    #endregion

    #region SetItem
    [ReducerMethod]
    public static RecipeState ReduceSetItemAction(RecipeState state, SetItemAction<IRecipeModel> action) =>
        state with { Item = action.Item, 
                     FilteredIngredients = state.FilteredIngredients.Where(c => state.Item?.IngredientModels?.FirstOrDefault(p => p.IngredientModel?.Id == c.Id) == null),
                     FilteredTags = state.FilteredTags.Where(c => state.Item?.TagModels?.FirstOrDefault(p => p.TagModel?.Id == c.Id) == null),
                   };
    #endregion

    #region LoadItemsSuccessAction<IIngredientModel>
    [ReducerMethod]
    public static RecipeState ReduceLoadItemsSuccessAction(RecipeState state, LoadItemsSuccessAction<IIngredientModel> action) => state with { FilteredIngredients = action.Items.Where(c => state.Item?.IngredientModels?.FirstOrDefault(p => p.IngredientId == c.Id) == null) };
    #endregion
    
    #region LoadItemsSuccessAction<ITagModel>
    [ReducerMethod]
    public static RecipeState ReduceLoadItemsSuccessAction(RecipeState state, LoadItemsSuccessAction<ITagModel> action) => state with { FilteredTags = action.Items.Where(c => state.Item?.TagModels?.FirstOrDefault(p => p.TagId == c.Id) == null) };
    #endregion

    #region DeleteRecipes
    [ReducerMethod]
    public static RecipeState ReduceDeleteItemsSuccessAction(RecipeState state, DeleteItemsSuccessAction<IRecipeModel> action) => state with { Items = state.Items.Where(c => !action.Items.Contains(c)) };
    #endregion

    #region SetLoadingAction
    [ReducerMethod]
    public static RecipeState ReduceSetLoadingAction(RecipeState state, SetLoadingAction<IRecipeModel> action) => state with { IsLoading = action.IsLoading };
    #endregion SetLoadingAction
}