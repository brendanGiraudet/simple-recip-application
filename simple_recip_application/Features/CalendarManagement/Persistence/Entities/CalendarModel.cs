using simple_recip_application.Data.Persistence.Entities;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Entities;
using simple_recip_application.Features.CalendarUserAccessManagement.Persistence.Entities;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipePlanningFeature.Persistence.Entities;

namespace simple_recip_application.Features.CalendarManagement.Persistence.Entities;

public class CalendarModel : EntityBase, ICalendarModel
{
    public string Name { get; set; } = default!;

    public ICollection<IPlanifiedRecipeModel> PlanifiedRecipeModels
    {
        get => PlanifiedRecipes.Cast<IPlanifiedRecipeModel>().ToList();
        set => PlanifiedRecipes = value.Cast<PlanifiedRecipeModel>().ToList();
    }

    public ICollection<PlanifiedRecipeModel> PlanifiedRecipes { get; set; } = [];

    public ICollection<ICalendarUserAccessModel> CalendarUserAccessModels 
    { 
        get => CalendarUsersAccess.Cast<ICalendarUserAccessModel>().ToList();
        set => CalendarUsersAccess = value.Cast<CalendarUserAccessModel>().ToList();
    }
    public ICollection<CalendarUserAccessModel> CalendarUsersAccess { get; set; } = [];

}