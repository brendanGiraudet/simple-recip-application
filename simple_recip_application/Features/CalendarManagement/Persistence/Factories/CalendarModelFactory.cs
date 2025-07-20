using simple_recip_application.Features.CalendarManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Factories;
using simple_recip_application.Features.CalendarManagement.Persistence.Entities;

namespace simple_recip_application.Features.CalendarManagement.Persistence.Factories;

public class CalendarModelFactory : ICalendarModelFactory
{
    public ICalendarModel CreateCalendarModel(string? name = null) => new CalendarModel
    {
        Name = name,
    };
}
