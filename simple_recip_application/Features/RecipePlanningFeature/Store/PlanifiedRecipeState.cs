using Fluxor;
using simple_recip_application.Extensions;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Entities;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Store;

namespace simple_recip_application.Features.RecipePlanningFeature.Store;

[FeatureState]
public record class PlanifiedRecipeState : BaseState<IPlanifiedRecipeModel>
{
    public Dictionary<DayOfWeek, List<IPlanifiedRecipeModel>> RecipesGroupedByDay { get; set; } = [];
    public DateTime CurrentWeekStart { get; set; } = DateTime.UtcNow.StartOfWeek(DayOfWeek.Monday);
    public ICalendarModel CurrentCalendar { get; set; }
}
