using simple_recip_application.Features.UserInfos.ApplicationCore.Entities;

namespace simple_recip_application.Features.UserInfos.Store.Actions;

public record SetUserInfosAction(IUserInfosModel UserInfo);
