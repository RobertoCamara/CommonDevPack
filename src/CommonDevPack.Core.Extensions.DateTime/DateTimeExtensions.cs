using NodaTime;

namespace CommonDevPack.Core.Extensions.DateTime;
public static class DateTimeExtensions
{
    public static System.DateTime ToZonedDateTime(this System.DateTime dateTime, string timeZoneName = "America/Sao_Paulo")
    {
        if (dateTime == default(System.DateTime))
            return dateTime;

        var timeZone = DateTimeZoneProviders.Tzdb[timeZoneName];
        

        var instant = Instant.FromDateTimeUtc(System.DateTime.SpecifyKind(dateTime, DateTimeKind.Utc));
        return instant.InZone(timeZone).ToDateTimeUnspecified();
    }
}