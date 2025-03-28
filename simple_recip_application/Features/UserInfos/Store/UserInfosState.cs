using Fluxor;
using simple_recip_application.Features.UserInfos.ApplicationCore.Entities;

namespace simple_recip_application.Features.UserInfos.Store;

[FeatureState]
public record class UserInfosState()
{
    public IUserInfosModel? UserInfo { get; set; }
}