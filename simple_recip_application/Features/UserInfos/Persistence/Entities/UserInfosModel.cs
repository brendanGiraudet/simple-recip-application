using simple_recip_application.Features.UserInfos.ApplicationCore.Entities;

namespace simple_recip_application.Features.UserInfos.Persistence.Entities;

public class UserInfosModel : IUserInfosModel
{
    public string Id { get; set; } = "anonymous";
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
}
