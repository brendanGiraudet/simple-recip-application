namespace simple_recip_application.Extensions;

public static class DateTimeExtensions
{
    public static DateTime StartOfWeek(this DateTime date, DayOfWeek startDay = DayOfWeek.Monday)
    {
        int diff = (7 + (date.DayOfWeek - startDay)) % 7;
        
        return date.Date.AddDays(-1 * diff);
    }
    public static DateTime EndOfWeek(this DateTime date, DayOfWeek endDay = DayOfWeek.Sunday)
    {
        int diff = (7 + (endDay - date.DayOfWeek)) % 7;

        return date.Date.AddDays(diff);
    }
}
