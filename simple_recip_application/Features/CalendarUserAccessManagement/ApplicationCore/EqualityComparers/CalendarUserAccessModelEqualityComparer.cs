using simple_recip_application.Features.CalendarUserAccessManagement.ApplicationCore.Entities;
using System.Diagnostics.CodeAnalysis;

namespace simple_recip_application.Features.RecipesManagement.ApplicationCore.EqualityComparers;

public class CalendarUserAccessModelEqualityComparer : IEqualityComparer<ICalendarUserAccessModel>
{
    public bool Equals(ICalendarUserAccessModel? x, ICalendarUserAccessModel? y)
    {
        if (x is null || y is null) return false;

        return Guid.Equals(x.CalendarId, y.CalendarId) &&
               string.Equals(x.UserId, y.UserId);
    }

    public int GetHashCode([DisallowNull] ICalendarUserAccessModel obj)
    {
        return obj.GetHashCode();
    }
}