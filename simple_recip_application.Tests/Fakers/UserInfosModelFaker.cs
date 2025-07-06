using Bogus;
using simple_recip_application.Features.UserInfos.Persistence.Entities;

namespace simple_recip_application.Tests.Fakers;

public class UserInfosModelFaker : Faker<UserInfosModel>
{
    public UserInfosModelFaker()
    {
        RuleFor(r => r.Email, f => f.Internet.Email());
        RuleFor(r => r.Firstname, f => f.Name.FirstName());
        RuleFor(r => r.Lastname, f => f.Name.LastName());
        RuleFor(r => r.Id, f => f.Random.String2(10));
    }
}
