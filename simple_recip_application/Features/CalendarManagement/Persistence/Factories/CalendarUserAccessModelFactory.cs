using simple_recip_application.Features.CalendarManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Factories;
using simple_recip_application.Features.CalendarManagement.Persistence.Entities;

namespace simple_recip_application.Features.CalendarManagement.Persistence.Factories;

public class CalendarUserAccessModelFactory : ICalendarUserAccessModelFactory
{
    public ICalendarUserAccessModel CreateCalendarUserAccessModel(string userId, ICalendarModel calendarModel) => new CalendarUserAccessModel
    {
        UserId = userId,
        CalendarModel = (CalendarModel)calendarModel,
        CalendarId = calendarModel?.Id ?? Guid.Empty,
    };
}
