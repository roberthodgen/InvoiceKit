using InvoiceKit.Domain.Invoice;

namespace InvoiceKit.Tests.Domain.InvoiceTests;

public sealed class InvoiceLineItemTests
{
    [Fact]
    public void InvoiceLineItem_CreateNew_SetsValues()
    {
        var lineItem = InvoiceLineItem.CreateNew(
            InvoiceLineItemDescription.CreateNew("This is a description."), 
            InvoiceLineItemPerUnitPrice.CreateNew(10), 
            InvoiceLineItemQuantity.CreateNew(10));
        lineItem.Description?.Value.ShouldBe("This is a description.");
        lineItem.PerUnitPrice.Amount.ShouldBe(10);
        lineItem.Quantity.Value.ShouldBe(10);
    }
}
