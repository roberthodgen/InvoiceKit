namespace InvoiceKit.Tests.Domain.Kernel;

using InvoiceKit.Domain.Kernel;

public class CurrencyTests
{
    [Fact]
    public void Currency_UnitedStatesDollar_HasCorrectValue()
    {
        ((int)Currency.UnitedStatesDollar).ShouldBe(0);
        nameof(Currency.UnitedStatesDollar).ShouldBe("UnitedStatesDollar");
    }

    [Fact]
    public void Currency_Unknown_HasCorrectValue()
    {
        ((int)Currency.Unknown).ShouldBe(1);
        nameof(Currency.Unknown).ShouldBe("Unknown");
    }

    [Fact]
    public void Currency_CanParse_ValidValues()
    {
        Currency parsed;
        Enum.TryParse("UnitedStatesDollar", out parsed).ShouldBeTrue();
        parsed.ShouldBe(Currency.UnitedStatesDollar);

        Enum.TryParse("Unknown", out parsed).ShouldBeTrue();
        parsed.ShouldBe(Currency.Unknown);
    }

    [Fact]
    public void Currency_CannotParse_InvalidValues()
    {
        Enum.TryParse("InvalidCurrency", out Currency _).ShouldBeFalse();
    }

    [Fact]
    public void Currency_DefaultValue_IsUnitedStatesDollar()
    {
        default(Currency).ShouldBe(Currency.UnitedStatesDollar);
    }
}
