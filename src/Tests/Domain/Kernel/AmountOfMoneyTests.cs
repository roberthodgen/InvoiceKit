namespace InvoiceKit.Tests.Domain.Kernel;

using InvoiceKit.Domain.Kernel;
using XunitExtensions;

public class AmountOfMoneyTests
{
    [Fact]
    public void AmountOfMoney_Constructor_SetsAmount()
    {
        new AmountOfMoney(99.99m, Currency.UnitedStatesDollar).Amount.ShouldBe(99.99m);
    }
    
    [Fact]
    public void AmountOfMoney_Constructor_SetsCurrency()
    {
        new AmountOfMoney(99.99m, Currency.UnitedStatesDollar).Currency.ShouldBe(Currency.UnitedStatesDollar);
    }

    [Fact]
    public void AmountOfMoney_ToString_FormatsUSD()
    {
        new AmountOfMoney(99.99m, Currency.UnitedStatesDollar).ToString().ShouldBe("$99.99");
    }

    [Fact]
    [UseCulture("en-US")]
    public void AmountOfMoney_ToString_UnknownEnUs()
    {
        new AmountOfMoney(99.99m).ToString().ShouldBe("$99.99");
    }
    
    [Fact]
    [UseCulture("en-GB")]
    public void AmountOfMoney_ToString_UnknownEnGb()
    {
        new AmountOfMoney(99.99m).ToString().ShouldBe("£99.99");
    }

    [Fact]
    [UseCulture("fr-FR")]
    public void AmountOfMoney_ToString_UnknownFrFr()
    {
        new AmountOfMoney(99.99m).ToString().ShouldBe("99,99 €");
    }
}
