namespace simple_recip_application.Features.CalendarManagement.ApplicationCore.Entities;

public interface ICalendarUserAccessModel
{
    public string UserId { get; set; }

    public Guid CalendarId { get; set; }
    public ICalendarModel CalendarModel { get; set; }
}
