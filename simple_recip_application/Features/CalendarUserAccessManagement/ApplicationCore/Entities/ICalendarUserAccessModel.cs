using simple_recip_application.Features.CalendarManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Entities;

public interface ICalendarUserAccessModel
{
    public string UserId { get; set; }
    public string UserEmail { get; set; }

    public Guid CalendarId { get; set; }
    public ICalendarModel CalendarModel { get; set; }
}
