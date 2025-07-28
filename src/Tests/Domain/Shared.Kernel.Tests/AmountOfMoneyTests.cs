using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.Shared.Kernel.Tests;

public sealed record AmountOfMoneyTests
{
    private sealed record AmountOfMoneyTest : AmountOfMoney
    {
        public AmountOfMoneyTest(decimal amount) : base(amount) { }
    }
    
    [Fact]
    public void AmountOfMoney_ZeroAmountOfMoney_IsAssignableToAmountOfMoney()
    {
        typeof(AmountOfMoney.ZeroAmountOfMoney).IsAssignableTo(typeof(AmountOfMoney)).ShouldBeTrue();
    }
    
    [Fact]
    public void AmountOfMoney_Zero_ReturnsZero()
    {
        var test = AmountOfMoney.Zero;
        test.Amount.ShouldBe(0m);
    }
    
    [Fact]
    public void AmountOfMoney_CreateNew_SetsValue()
    {
        var test = new AmountOfMoneyTest(100m);
        test.Amount.ShouldBe(100m);
    }
    
    [Fact]
    public void AmountOfMoney_ToString_ReturnsFormattedString()
    {
        var test = new AmountOfMoneyTest(1000.10m);
        test.ToString().ShouldBeOneOf("$1,000.10", "¤1,000.10");
    }
}
