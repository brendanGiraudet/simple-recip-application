using Fluxor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using simple_recip_application.Features.UserInfos.Store.Actions;

namespace simple_recip_application.Features.UserInfos.ApplicationCore.AuthenticationStateProvider;

public class CustomAuthenticationStateProvider
(
     IDispatcher _dispatcher
)
: ServerAuthenticationStateProvider
{

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authState = await base.GetAuthenticationStateAsync();

        var user = authState.User;

        if (user.Identity?.IsAuthenticated == true)
            _dispatcher.Dispatch(new LoadUserInfosFromAuthenticationStateAction(user));

        return authState;
    }
}
