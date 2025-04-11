using simple_recip_application.Features.TagsManagement.ApplicationCore.Entities;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Factories;
using simple_recip_application.Features.TagsManagement.Persistence.Entities;

namespace simple_recip_application.Features.TagsManagement.Persistence.Factories;

public class TagFactory : ITagFactory
{
    public ITagModel Create(string name) => new TagModel { Name = name };
}
