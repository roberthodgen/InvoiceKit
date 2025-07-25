using InvoiceKit.Domain.Client;
using InvoiceKit.Domain.Company;
using InvoiceKit.Domain.Invoice;
using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.InvoiceTests;

public sealed class InvoiceTests
{
    private readonly Company _company = Company.CreateNew(
        new CompanyName("Company LLC"),
        new CompanyContactName("The Company"),
        CompanyEmail.CreateNew("company@company.com"),
        CompanyPhone.CreateNew("123-456-7890"),
        CompanyAddress.CreateNew("123 street", null, "city", "state", "zip", "US"),
        Client.CreateNew(
            new ClientName("Client"),
            new ClientContactName("The Client"),
            ClientEmail.CreateNew("client@mail.com"),
            ClientPhone.CreateNew("123-456-7890"),
            ClientAddress.CreateNew("321 street", null, "city", "state", "zip", "US")));
    private static readonly ISystemClock SystemClock = ManualSystemClock.CreateNew(DateTime.UtcNow);
    private static readonly IInvoiceTerms Terms = InvoiceStandardTerms.CreateNewDaysFromNow(SystemClock, 10);

    [Fact]
    public void Invoice_AddLineItem_CreatesALineItem()
    {
        var invoice = _company.CreateInvoiceForClient(
            _company.Client, 
            InvoiceDueDate.CreateNew(Terms), 
            InvoiceNumber.CreateNew("abc123"));
        invoice.AddLineItem(InvoiceLineItem.CreateNew(
            InvoiceLineItemDescription.CreateNew("Ten items at $10.50 each."), 
            InvoiceLineItemPerUnitPrice.CreateNew(10.50m), 
            InvoiceLineItemQuantity.CreateNew(10)));
        invoice.Items.Count.ShouldBe(1);
        invoice.InvoiceNumber.Value.ShouldBe("abc123");
        var lineItem = invoice.Items.First();
        lineItem.Description.Value.ShouldBe("Ten items at $10.50 each.");
        lineItem.PerUnitPrice.Amount.ShouldBe(10.50m);
        lineItem.Quantity.Value.ShouldBe(10);
    }
    
}