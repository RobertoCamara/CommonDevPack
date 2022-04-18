using FluentAssertions;
using Xunit;

namespace CommonDevPack.Core.Extensions.DateTime.Tests;
public class DatetimeExtensionsTest
{
    [Fact(DisplayName = nameof(UtcNow_ToZonedDateTime_IsDefault))]
    [Trait("Core.Extensions", "Datetime")]
    public void UtcNow_ToZonedDateTime_IsDefault()
    {
        var utcNow = default(System.DateTime);

        var utcNowToZoneDatetime = utcNow.ToZonedDateTime();

        utcNowToZoneDatetime.Should().Be(System.DateTime.MinValue);
        utcNowToZoneDatetime.Should().BeSameDateAs(utcNow);
    }


    [Fact(DisplayName = nameof(UtcNow_ToZonedDateTime_InAmericaSaoPaulo))]
    [Trait("Core.Extensions", "Datetime")]
    public void UtcNow_ToZonedDateTime_InAmericaSaoPaulo()
    {
        var utcNow = System.DateTime.UtcNow;

        var utcNowToZoneDatetime = utcNow.ToZonedDateTime();

        utcNowToZoneDatetime.ToUniversalTime().Should().Be(utcNow);
        utcNowToZoneDatetime.Hour.Should().Be(utcNow.Hour - 3);
    }

    [Fact(DisplayName = nameof(UtcNow_ToZonedDateTime_InAmericaManaus))]
    [Trait("Core.Extensions", "Datetime")]
    public void UtcNow_ToZonedDateTime_InAmericaManaus()
    {
        var utcNow = System.DateTime.UtcNow;

        var utcNowToZoneDatetime = utcNow.ToZonedDateTime("America/Manaus");

        utcNowToZoneDatetime.ToUniversalTime().Should().Be(utcNow.AddHours(-1));
        utcNowToZoneDatetime.Hour.Should().Be(utcNow.Hour - 4);
    }

    [Fact(DisplayName = nameof(UtcNow_ToZonedDateTime_InAmericaVancouver))]
    [Trait("Core.Extensions", "Datetime")]
    public void UtcNow_ToZonedDateTime_InAmericaVancouver()
    {
        var utcNow = System.DateTime.UtcNow;

        var utcNowToZoneDatetime = utcNow.ToZonedDateTime("America/Vancouver");

        utcNowToZoneDatetime.ToUniversalTime().Should().Be(utcNow.AddHours(- 4));
        utcNowToZoneDatetime.Hour.Should().Be(utcNow.Hour - 7);
    }

    [Fact(DisplayName = nameof(UtcNow_ToZonedDateTime_InEuropeParis))]
    [Trait("Core.Extensions", "Datetime")]
    public void UtcNow_ToZonedDateTime_InEuropeParis()
    {
        var utcNow = System.DateTime.UtcNow;

        var utcNowToZoneDatetime = utcNow.ToZonedDateTime("Europe/Paris");

        utcNowToZoneDatetime.ToUniversalTime().Should().Be(utcNow.AddHours( + 5));
        utcNowToZoneDatetime.Hour.Should().Be(utcNow.Hour + 2);
    }
}