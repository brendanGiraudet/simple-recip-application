using Fluxor;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Entities;
using simple_recip_application.Store.Actions;

public static class TagReducer
{
    [ReducerMethod]
    public static TagState OnLoad(TagState state, LoadItemsAction<ITagModel> _) => state with { IsLoading = true };

    [ReducerMethod]
    public static TagState OnLoadSuccess(TagState state, LoadItemsSuccessAction<ITagModel> action)
        => state with { Items = action.Items, IsLoading = false };

    [ReducerMethod]
    public static TagState OnLoadFailure(TagState state, LoadItemsFailureAction<ITagModel> _) => state with { IsLoading = false };
}
