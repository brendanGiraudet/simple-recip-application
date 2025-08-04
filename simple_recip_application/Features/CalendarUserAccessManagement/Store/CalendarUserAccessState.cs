using Fluxor;
using simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Entities;
using simple_recip_application.Store;

namespace simple_recip_application.Features.CalendarUserAccessManagement.Store;

[FeatureState]
public record class CalendarUserAccessState : BaseState<ICalendarUserAccessModel>
{
}
