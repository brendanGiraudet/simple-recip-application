using System.Diagnostics.CodeAnalysis;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;

namespace simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.EqualityComparers;

public class PlanifiedRecipeEqualityComparer : IEqualityComparer<IPlanifiedRecipeModel>
{
    public bool Equals(IPlanifiedRecipeModel? x, IPlanifiedRecipeModel? y)
    {
        if(x is null || y is null) return false;

        return string.Equals(x.MomentOftheDay, y.MomentOftheDay) &&
               Guid.Equals(x.RecipeId, y.RecipeId) &&
               DateTime.Equals(x.PlanifiedDateTime, y.PlanifiedDateTime) &&
               Guid.Equals(x.CalendarId, y.CalendarId);
    }

    public int GetHashCode([DisallowNull] IPlanifiedRecipeModel obj)
    {
        return obj.GetHashCode();
    }
}