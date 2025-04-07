using System.Security.Claims;

namespace simple_recip_application.Features.UserInfos.Store.Actions;

public record LoadUserInfosFromAuthenticationStateAction(ClaimsPrincipal ClaimsPrincipal);
