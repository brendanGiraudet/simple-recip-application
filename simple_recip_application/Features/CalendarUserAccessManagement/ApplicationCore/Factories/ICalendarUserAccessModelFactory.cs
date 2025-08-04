using simple_recip_application.Features.CalendarManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Factories;

public interface ICalendarUserAccessModelFactory
{
    ICalendarUserAccessModel CreateCalendarUserAccessModel(string userId, string userEmail, Guid calendarId, ICalendarModel? calendarModel = null);
}
