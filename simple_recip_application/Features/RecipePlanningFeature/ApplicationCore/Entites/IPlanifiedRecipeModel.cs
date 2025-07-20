using simple_recip_application.Features.CalendarManagement.ApplicationCore.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;

namespace simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;

public interface IPlanifiedRecipeModel
{
    public Guid RecipeId { get; set; }
    public IRecipeModel RecipeModel { get; set; }

    public DateTime PlanifiedDateTime { get; set; }

    public Guid? CalendarId { get; set; }
    public ICalendarModel? CalendarModel { get; set; }
    
    public string? MomentOftheDay { get; set; }
}