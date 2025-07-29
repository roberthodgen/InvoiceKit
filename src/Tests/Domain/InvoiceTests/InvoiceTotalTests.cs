using InvoiceKit.Domain.Client;
using InvoiceKit.Domain.Company;
using InvoiceKit.Domain.Invoice;
using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.InvoiceTests;

public sealed class InvoiceTotalTests
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
    public void InvoiceTotal_IsA_AmountOfMoney()
    {
        typeof(InvoiceTotal).IsAssignableTo(typeof(AmountOfMoney)).ShouldBeTrue();
    }
    
    [Fact]
    public void InvoiceTotal_CreateNew_SetsValue()
    {
        var invoice = _company.CreateInvoiceForClient(
            _company.Client, 
            InvoiceDueDate.CreateNew(Terms), 
            InvoiceNumber.CreateNew("abc123"));
        invoice.AddLineItem(InvoiceLineItem.CreateNew(
            InvoiceLineItemDescription.CreateNew("Ten items at $10.50 each."), 
            InvoiceLineItemPerUnitPrice.CreateNew(10.50m), 
            InvoiceLineItemQuantity.CreateNew(10)));
        invoice.AddLineItem(InvoiceLineItem.CreateNew(
            InvoiceLineItemDescription.CreateNew("Five items at $100.00 each."), 
            InvoiceLineItemPerUnitPrice.CreateNew(100.00m), 
            InvoiceLineItemQuantity.CreateNew(5)));
        invoice.Total.Amount.ShouldBe(605.00m);
    }
}
