using InvoiceKit.Domain.Client;
using InvoiceKit.Domain.Company;
using InvoiceKit.Domain.Invoice;
using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.InvoiceTests;

public sealed class InvoiceLineItemSubTotalTests
{
    [Fact]
    public void InvoiceLineItemSubTotal_IsA_AmountOfMoney()
    {
        typeof(InvoiceLineItemSubTotal).IsAssignableTo(typeof(AmountOfMoney)).ShouldBeTrue();
    }
    
    [Fact]
    public void InvoiceLineItemSubTotal_CreateNew_SetsValue()
    {
        var lineItem = InvoiceLineItem.CreateNew(
            InvoiceLineItemDescription.CreateNew("Ten items at 10.50 each."), 
            InvoiceLineItemPerUnitPrice.CreateNew(10.50m), 
            InvoiceLineItemQuantity.CreateNew(10));
        lineItem.SubTotal.Amount.ShouldBe(105.00m);
    }
}
