namespace simple_recip_application.Features.CalendarUserAccessManagement.Store.Actions;

public record ShareCalendarAction(string UserEmail, string CalendarName, string AcceptanceUrl) { }
public record ShareCalendarSuccessAction { }
public record ShareCalendarFailureAction { }
