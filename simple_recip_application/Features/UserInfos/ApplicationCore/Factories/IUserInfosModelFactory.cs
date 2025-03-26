using simple_recip_application.Features.UserInfos.ApplicationCore.Entities;

namespace simple_recip_application.Features.UserInfos.ApplicationCore.Factories;

public interface IUserInfosModelFactory
{
    public IUserInfosModel Create(string? id = null, string? firstname = null, string? lastname = null, string? email = null);
}
