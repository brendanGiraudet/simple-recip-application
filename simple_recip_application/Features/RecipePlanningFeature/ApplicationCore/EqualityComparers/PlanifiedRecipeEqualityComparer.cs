using System.Diagnostics.CodeAnalysis;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.EqualityComparers;

namespace simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.EqualityComparers;

public class PlanifiedRecipeEqualityComparer : IEqualityComparer<IPlanifiedRecipeModel>
{
    public bool Equals(IPlanifiedRecipeModel? x, IPlanifiedRecipeModel? y)
    {
        if(x is null || y is null) return false;

        return x.MomentOftheDay?.Equals(y.MomentOftheDay ?? string.Empty) ?? false &&
               x.RecipeId.Equals(y.RecipeId) &&
               x.PlanifiedDateTime.Equals(y.PlanifiedDateTime) &&
               x.UserId.Equals(y.UserId);
    }

    public int GetHashCode([DisallowNull] IPlanifiedRecipeModel obj)
    {
        return obj.GetHashCode();
    }
}