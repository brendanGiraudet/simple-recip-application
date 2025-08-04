using simple_recip_application.Features.CalendarManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarManagement.Persistence.Entities;
using simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Factories;
using simple_recip_application.Features.CalendarUserAccessManagement.Persistence.Entities;

namespace simple_recip_application.Features.CalendarUserAccessManagement.Persistence.Factories;

public class CalendarUserAccessModelFactory : ICalendarUserAccessModelFactory
{
    public ICalendarUserAccessModel CreateCalendarUserAccessModel(string userId, string userEmail, Guid calendarId, ICalendarModel? calendarModel = null) => new CalendarUserAccessModel
    {
        UserId = userId,
        UserEmail = userEmail,
        CalendarModel = (CalendarModel)calendarModel,
        CalendarId = calendarId,
    };
}
