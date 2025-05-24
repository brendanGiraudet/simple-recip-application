using Fluxor;
using simple_recip_application.Features.NotificationsManagement.Store;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.TagsManagement.ApplicationCore.EqualityComparers;
using simple_recip_application.Store.Actions;

public static class TagReducer
{
    #region Load
    [ReducerMethod]
    public static TagState OnLoadItemsAction(TagState state, LoadItemsAction<ITagModel> _) => state with { IsLoading = true };

    [ReducerMethod]
    public static TagState OnLoadItemsSuccessAction(TagState state, LoadItemsSuccessAction<ITagModel> action)
        => state with { Items = action.Items, IsLoading = false };

    [ReducerMethod]
    public static TagState OnLoadItemsFailureAction(TagState state, LoadItemsFailureAction<ITagModel> _) => state with { IsLoading = false };
    #endregion

    #region Add
    [ReducerMethod]
    public static TagState OnAddItemAction(TagState state, AddItemAction<ITagModel> _) => state with { IsLoading = true };

    [ReducerMethod]
    public static TagState OnAddItemSuccessAction(TagState state, AddItemSuccessAction<ITagModel> action) => state with { IsLoading = false, Items = state.Items.Append(action.Item) };

    [ReducerMethod]
    public static TagState OnAddItemFailureAction(TagState state, AddItemFailureAction<ITagModel> _) => state with { IsLoading = false };
    #endregion
    
    #region Delete
    [ReducerMethod]
    public static TagState OnDeleteItemAction(TagState state, DeleteItemAction<ITagModel> _) => state with { IsLoading = true };

    [ReducerMethod]
    public static TagState OnDeleteItemSuccessAction(TagState state, DeleteItemSuccessAction<ITagModel> action) => state with { IsLoading = false, Items = state.Items.Where(t => !new TagEqualityComparer().Equals(t, action.Item)) };

    [ReducerMethod]
    public static TagState OnDeleteItemFailureAction(TagState state, DeleteItemFailureAction<ITagModel> _) => state with { IsLoading = false };
    #endregion

    [ReducerMethod]
    public static TagState OnSetSearchTermAction(TagState state, SetSearchTermAction<ITagModel> action) => state with { SearchTerm = action.SearchTerm };
}
