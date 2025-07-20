using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entites;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Factories;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Entities;

namespace simple_recip_application.Features.RecipePlanningFeature.Persistence.Factories;

public class CalendarModelFactory : ICalendarModelFactory
{
    public ICalendarModel CreateCalendarModel(string name) => new CalendarModel
    {
        Name = name,
    };
}
