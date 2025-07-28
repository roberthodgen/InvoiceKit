using InvoiceKit.Domain.Invoice;
using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.InvoiceTests;

public sealed class InvoiceLineItemPerUnitPriceTests
{
    [Fact]
    public void InvoiceLineItemPerUnitPrice_IsA_AmountOfMoney()
    {
        typeof(InvoiceLineItemPerUnitPrice).IsAssignableTo(typeof(AmountOfMoney)).ShouldBeTrue();
    }

    [Fact]
    public void InvoiceLineItemPerUnitPrice_CreateNew_SetsValue()
    {
        var perUnitPrice = InvoiceLineItemPerUnitPrice.CreateNew(100);
        perUnitPrice.Amount.ShouldBe(100);
    }
}
