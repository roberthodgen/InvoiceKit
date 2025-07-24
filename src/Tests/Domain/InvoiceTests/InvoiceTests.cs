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
    [Fact]
    public void Invoice_CreateNew_SetsValues()
    {
        var systemClock = ManualSystemClock.CreateNew(DateTime.UtcNow);
        var terms = InvoiceStandardTerms.CreateNewDaysFromNow(systemClock, 10);
        var invoice = _company.CreateInvoiceForClient(
            _company.Client, 
            InvoiceDueDate.CreateNew(terms), 
            InvoiceNumber.CreateNew("abc123"));
        invoice.Client.ShouldBe(_company.Client);
        invoice.Company.ShouldBe(_company);
        invoice.DueDate.Value.ShouldBe(systemClock.Now.AddDays(10));
    }
}