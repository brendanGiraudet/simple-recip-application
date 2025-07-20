using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entites;

namespace simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Factories;

public interface ICalendarUserAccessModelFactory
{
    ICalendarUserAccessModel CreateCalendarUserAccessModel(string userId, ICalendarModel calendarModel);
}
