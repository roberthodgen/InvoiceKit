namespace InvoiceKit.Tests.Domain.InvoiceTests;

using InvoiceKit.Domain.Invoice;
using InvoiceKit.Domain.Shared.Kernel;

public sealed class InvoiceLineItemSubtotalTests
{
    [Fact]
    public void InvoiceLineItemSubtotal_IsA_AmountOfMoney()
    {
        typeof(InvoiceLineItemSubtotal).IsAssignableTo(typeof(AmountOfMoney)).ShouldBeTrue();
    }
    
    [Fact]
    public void InvoiceLineItemSubtotal_CreateNew_SetsValue()
    {
        var lineItem = InvoiceLineItem.CreateNew(
            InvoiceLineItemDescription.CreateNew("Ten items at 10.50 each."), 
            InvoiceLineItemPerUnitPrice.CreateNew(10.50m),
            InvoiceLineItemQuantity.CreateNew(10));
        lineItem.Subtotal.Amount.ShouldBe(105.00m);
    }
}
