using Microsoft.AspNetCore.Components;

namespace simple_recip_application.Emails.Templates.AddCalendarUserAccessTemplate;

public partial class AddCalendarUserAccessTemplate
{
    [Parameter] public string EmailSender { get; set; }
    [Parameter] public string CalendarName { get; set; }
    [Parameter] public string AcceptanceUrl { get; set; }
}
