using System.Diagnostics.CodeAnalysis;
using simple_recip_application.Features.TagsManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.TagsManagement.ApplicationCore.EqualityComparers;

public class TagEqualityComparer : IEqualityComparer<ITagModel>
{
    public bool Equals(ITagModel? x, ITagModel? y)
    {
        if(x is null || y is null) return false;

        return string.Equals(x.Name, y.Name) &&
               Guid.Equals(x.Id, y.Id);
    }

    public int GetHashCode([DisallowNull] ITagModel obj)
    {
        return obj.GetHashCode();
    }
}