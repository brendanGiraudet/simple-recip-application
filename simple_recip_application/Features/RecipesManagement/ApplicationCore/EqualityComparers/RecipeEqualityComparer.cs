using System.Diagnostics.CodeAnalysis;
using simple_recip_application.Features.RecipesManagement.ApplicationCore.Entites;

namespace simple_recip_application.Features.RecipesManagement.ApplicationCore.EqualityComparers;

public class RecipeEqualityComparer : IEqualityComparer<IRecipeModel>
{
    public bool Equals(IRecipeModel? x, IRecipeModel? y)
    {
        if(x is null || y is null) return false;

        return string.Equals(x.Category, y.Category) &&
               DateTime.Equals(x.CookingTime, y.CookingTime) &&
               DateTime.Equals(x.PreparationTime, y.PreparationTime) &&
               string.Equals(x.Name, y.Name);
    }

    public int GetHashCode([DisallowNull] IRecipeModel obj)
    {
        return obj.GetHashCode();
    }
}