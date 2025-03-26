namespace simple_recip_application.Features.UserInfos.ApplicationCore.Entities;

public interface IUserInfosModel
{
    public string Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
}
