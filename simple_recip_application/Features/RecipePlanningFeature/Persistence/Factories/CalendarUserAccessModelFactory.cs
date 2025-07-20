using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entites;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Factories;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Entites;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Entities;

namespace simple_recip_application.Features.RecipePlanningFeature.Persistence.Factories;

public class CalendarUserAccessModelFactory : ICalendarUserAccessModelFactory
{
    public ICalendarUserAccessModel CreateCalendarUserAccessModel(string userId, ICalendarModel calendarModel) => new CalendarUserAccessModel
    {
        UserId = userId,
        CalendarModel = (CalendarModel)calendarModel
    };
}
