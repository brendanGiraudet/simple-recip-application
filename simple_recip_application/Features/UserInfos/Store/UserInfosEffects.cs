using System.Security.Claims;
using Fluxor;
using simple_recip_application.Features.UserInfos.ApplicationCore.Factories;
using simple_recip_application.Features.UserInfos.Store.Actions;

namespace simple_recip_application.Features.UserInfos.Store;

public class UserInfosEffects
(
    IUserInfosModelFactory _userInfosModelFactory
)
{
    [EffectMethod]
    public async Task HandleLoadUserInfosFromAuthenticationStateAction(LoadUserInfosFromAuthenticationStateAction action, IDispatcher dispatcher)
    {
        var user = action.ClaimsPrincipal;

        var userInfo = _userInfosModelFactory.Create(
            id: user.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            email: user.FindFirst(ClaimTypes.Email)?.Value,
            firstname: user.FindFirst(ClaimTypes.GivenName)?.Value,
            lastname: user.FindFirst(ClaimTypes.Surname)?.Value
        );

        dispatcher.Dispatch(new SetUserInfosAction(userInfo));
    }
}
