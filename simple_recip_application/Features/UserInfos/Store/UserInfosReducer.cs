using Fluxor;
using simple_recip_application.Features.UserInfos.Store.Actions;

namespace simple_recip_application.Features.UserInfos.Store;

public static class UserInfosReducer
{
    [ReducerMethod]
    public static UserInfosState ReduceSetUserInfosAction(UserInfosState state, SetUserInfosAction action)
    => state with { UserInfo = action.UserInfo };

}