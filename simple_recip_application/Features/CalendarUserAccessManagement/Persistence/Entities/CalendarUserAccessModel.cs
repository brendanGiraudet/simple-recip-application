using simple_recip_application.Features.CalendarManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarManagement.Persistence.Entities;
using simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.CalendarUserAccessManagement.Persistence.Entities;

public class CalendarUserAccessModel : ICalendarUserAccessModel
{
    public string UserId { get; set; } = default!;
    public string UserEmail { get; set; } = default!;

    public Guid CalendarId { get; set; }
    public ICalendarModel CalendarModel { get => Calendar; set => Calendar = (CalendarModel)value; }
    public CalendarModel Calendar { get; set; }
}
