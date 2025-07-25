using InvoiceKit.Domain.Invoice;
using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.InvoiceTests;

public sealed class InvoiceLineItemPerUnitPriceTests
{
    [Fact]
    public void InvoiceLineItemPerUnitPrice_IsA_AmountOfMoney()
    {
        var perUnitPrice = InvoiceLineItemPerUnitPrice.CreateNew(100);
        Assert.IsAssignableFrom<AmountOfMoney>(perUnitPrice);
    }

    [Fact]
    public void InvoiceLineItemPerUnitPrice_CreateNew_SetsValue()
    {
        var perUnitPrice = InvoiceLineItemPerUnitPrice.CreateNew(100);
        perUnitPrice.Amount.ShouldBe(100);
    }
}