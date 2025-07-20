using simple_recip_application.Data.ApplicationCore.Entities;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;

namespace simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entites;

public interface ICalendarModel : IEntityBase
{
    public string Name { get; set; }

    public ICollection<IPlanifiedRecipeModel> PlanifiedRecipeModels { get; set; }

    public ICollection<ICalendarUserAccessModel> CalendarUserAccessModels { get; set; }
}
