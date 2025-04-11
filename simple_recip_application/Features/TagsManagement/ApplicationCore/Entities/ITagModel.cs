using simple_recip_application.Data.ApplicationCore.Entities;

namespace simple_recip_application.Features.TagsManagement.ApplicationCore.Entities;

public interface ITagModel : IEntityBase
{
    string Name { get; set; }
}