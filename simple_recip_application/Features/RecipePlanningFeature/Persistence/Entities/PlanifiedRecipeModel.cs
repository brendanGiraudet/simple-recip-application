using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entites;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;
using simple_recip_application.Features.RecipesManagement.Persistence.Entites;

namespace simple_recip_application.Features.RecipePlanningFeature.Persistence.Entities;

public class PlanifiedRecipeModel : IPlanifiedRecipeModel
{
    public Guid RecipeId { get; set; }
    public IRecipeModel RecipeModel
    {
        get => Recipe;
        set => Recipe = (RecipeModel)value;
    }
    public RecipeModel Recipe { get; set; } = default!;

    public DateTime PlanifiedDateTime { get; set; }

    public Guid? CalendarId { get; set; }
    public ICalendarModel? CalendarModel
    {
        get => Calendar;
        set => Calendar = (CalendarModel?)value;
    }
    public CalendarModel? Calendar { get; set; }

    public string? MomentOftheDay { get; set; }
}