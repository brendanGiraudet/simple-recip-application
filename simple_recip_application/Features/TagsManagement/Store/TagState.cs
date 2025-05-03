using Fluxor;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Entities;
using simple_recip_application.Store;

namespace simple_recip_application.Features.NotificationsManagement.Store;

[FeatureState]
public record TagState : EntityBaseState<ITagModel>
{
    
}
