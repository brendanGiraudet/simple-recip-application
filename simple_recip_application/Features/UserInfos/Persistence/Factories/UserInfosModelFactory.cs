using simple_recip_application.Features.UserInfos.ApplicationCore.Entities;
using simple_recip_application.Features.UserInfos.ApplicationCore.Factories;
using simple_recip_application.Features.UserInfos.Persistence.Entities;

namespace simple_recip_application.Features.UserInfos.Persistence.Factories;

public class UserInfosModelFactory : IUserInfosModelFactory
{
    public IUserInfosModel Create(string? id = null, string? firstname = null, string? lastname = null, string? email = null)
    {
        return new UserInfosModel
        {
            Id = id ?? "anonymous",
            Firstname = firstname ?? string.Empty,
            Lastname = lastname ?? string.Empty,
            Email = email ?? string.Empty
        };
    }
}
