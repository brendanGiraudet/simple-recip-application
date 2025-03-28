using System.Security.Claims;
using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using simple_recip_application.Features.UserInfos.ApplicationCore.Factories;
using simple_recip_application.Features.UserInfos.Store.Actions;

namespace simple_recip_application.Features.UserInfos.ApplicationCore.AuthenticationStateProvider;

public class CustomAuthenticationStateProvider
    (
        IDispatcher _dispatcher,
        IUserInfosModelFactory _userInfosModelFactory)
: ServerAuthenticationStateProvider
{

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authState = await base.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity?.IsAuthenticated == true)
        {
            var userInfo = _userInfosModelFactory.Create(
                id: user.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                email: user.FindFirst(ClaimTypes.Email)?.Value,
                firstname: user.FindFirst(ClaimTypes.GivenName)?.Value,
                lastname: user.FindFirst(ClaimTypes.Surname)?.Value
            );

            _dispatcher.Dispatch(new SetUserInfosAction(userInfo));
        }

        return authState;
    }
}
