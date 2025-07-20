using Fluxor;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarManagement.Persistence.Entities;
using simple_recip_application.Store;

namespace simple_recip_application.Features.CalendarManagement.Store;

[FeatureState]
public record class CalendarState : EntityBaseState<ICalendarModel>
{
    private CalendarState()
    {
        Item = new CalendarModel();
    }
}
