using System.Security.Claims;
using Fluxor;
using Microsoft.AspNetCore.Components.Server;
using simple_recip_application.Features.UserInfos.ApplicationCore.Factories;
using simple_recip_application.Features.UserInfos.Store;

namespace simple_recip_application.Features.UserInfos.ApplicationCore.AuthenticationStateProvider;

public class CustomAuthenticationStateProvider

: ServerAuthenticationStateProvider
{
    public CustomAuthenticationStateProvider(IDispatcher dispatcher, IUserInfosModelFactory _userInfosModelFactory, IState<UserInfosState> _userInfoState)
        : base()
    {
        AuthenticationStateChanged += async task =>
        {
            var authState = await task;
            if (authState.User.Identity?.IsAuthenticated == true && _userInfoState.Value.UserInfo is null)
            {
                var user = authState.User;
                var userInfo = _userInfosModelFactory.Create(
                    id: user.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    email: user.FindFirst(ClaimTypes.Email)?.Value,
                    firstname: user.FindFirst(ClaimTypes.GivenName)?.Value,
                    lastname: user.FindFirst(ClaimTypes.Surname)?.Value
                );

                _userInfoState.Value.UserInfo = userInfo;
            }
        };
    }
}