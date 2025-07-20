namespace simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entites;

public interface ICalendarUserAccessModel
{
    public string UserId { get; set; }

    public Guid CalendarId { get; set; }
    public ICalendarModel CalendarModel { get; set; }
}
