using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.Shared.Kernel.Tests;

public sealed record AmountOfMoneyTests
{
    private sealed record AmountOfMoneyTest : AmountOfMoney
    {
        public AmountOfMoneyTest(decimal amount) 
            : base(amount)
        {
            
        }
    }
    
    [Fact]
    public void AmountOfMoneyBase_ZeroMoney_Exists()
    {
        typeof(AmountOfMoney.ZeroAmountOfMoney).IsAssignableTo(typeof(AmountOfMoney)).ShouldBeTrue();
    }

    [Fact]
    public void AmountOfMoney_ZeroAmountOfMoney_ReturnsZero()
    {
       AmountOfMoney.ZeroAmountOfMoney.GetInstance.Amount.ShouldBe(0);
    }
    
    [Fact]
    public void AmountOfMoney_CreateNew_SetsValue()
    {
        var test = new AmountOfMoneyTest(100m);
        test.Amount.ShouldBe(100m);
    }
}