using simple_recip_application.Features.TagsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.TagsManagement.ApplicationCore.Factories;

public interface ITagFactory
{
    ITagModel Create(string name);
}
