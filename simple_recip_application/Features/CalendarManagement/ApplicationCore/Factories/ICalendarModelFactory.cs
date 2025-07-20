using simple_recip_application.Features.CalendarManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.CalendarManagement.ApplicationCore.Factories;

public interface ICalendarModelFactory
{
    ICalendarModel CreateCalendarModel(string? name = null);
}
