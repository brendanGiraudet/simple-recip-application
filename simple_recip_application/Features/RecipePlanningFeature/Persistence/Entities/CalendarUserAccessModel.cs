using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entites;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Entities;

namespace simple_recip_application.Features.RecipePlanningFeature.Persistence.Entites;

public class CalendarUserAccessModel : ICalendarUserAccessModel
{
    public string UserId { get; set; } = default!;

    public Guid CalendarId { get; set; }
    public ICalendarModel CalendarModel { get => Calendar; set => Calendar = (CalendarModel)value; }
    public CalendarModel Calendar { get; set; }
}
