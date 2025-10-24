using InvoiceKit.Domain.Company;
using InvoiceKit.Domain.Invoice;
using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.InvoiceTests;

public sealed class InvoiceTests
{
    private readonly Company _company = Company.CreateNew(new CompanyName("Company LLC"));
    private static readonly ISystemClock SystemClock = ManualSystemClock.CreateNew(DateTime.UtcNow);
    private static readonly IInvoiceTerms Terms = InvoiceStandardTerms.CreateNewDaysFromNow(SystemClock, 10);

    [Fact]
    public void Invoice_AddLineItem_CreatesLineItem()
    {
        var invoice = _company.CreateInvoiceWithoutClient(
            InvoiceDueDate.CreateNew(Terms),
            InvoiceNumber.CreateNew("abc123"),
            null);
        invoice.AddLineItem(InvoiceLineItem.CreateNew(
            InvoiceLineItemDescription.CreateNew("Ten items at $10.50 each."), 
            InvoiceLineItemPerUnitPrice.CreateNew(10.50m), 
            InvoiceLineItemQuantity.CreateNew(10)));
        invoice.Items.Count.ShouldBe(1);
        invoice.InvoiceNumber.Value.ShouldBe("abc123");
        var lineItem = invoice.Items.First();
        lineItem.Description?.Value.ShouldBe("Ten items at $10.50 each.");
        lineItem.PerUnitPrice.Amount.ShouldBe(10.50m);
        lineItem.Quantity.Value.ShouldBe(10);
    }
    
}
