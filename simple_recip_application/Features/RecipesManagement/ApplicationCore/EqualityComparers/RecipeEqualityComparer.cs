using System.Diagnostics.CodeAnalysis;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;

namespace simple_recip_application.Features.RecipesManagement.ApplicationCore.EqualityComparers;

public class RecipeEqualityComparer : IEqualityComparer<IRecipeModel>
{
    public bool Equals(IRecipeModel? x, IRecipeModel? y)
    {
        if(x is null || y is null) return false;

        return x.Category.Equals(y.Category) &&
               x.CookingTime.Equals(y.CookingTime) &&
               x.PreparationTime.Equals(y.PreparationTime) &&
               x.Name.Equals(y.Name);
    }

    public int GetHashCode([DisallowNull] IRecipeModel obj)
    {
        return obj.GetHashCode();
    }
}