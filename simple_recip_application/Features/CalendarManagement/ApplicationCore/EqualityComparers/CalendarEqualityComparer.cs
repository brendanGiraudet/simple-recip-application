using System.Diagnostics.CodeAnalysis;
using simple_recip_application.Features.CalendarManagement.ApplicationCore.Entities;

namespace simple_recip_application.Features.CalendarManagement.ApplicationCore.EqualityComparers;

public class CalendarEqualityComparer : IEqualityComparer<ICalendarModel>
{
    public bool Equals(ICalendarModel? x, ICalendarModel? y)
    {
        if (x is null || y is null) return false;

        return string.Equals(x.Name, y.Name);
    }

    public int GetHashCode([DisallowNull] ICalendarModel obj)
    {
        return obj.GetHashCode();
    }
}