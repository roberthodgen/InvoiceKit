using InvoiceKit.Domain.Client;
using InvoiceKit.Domain.Company;
using InvoiceKit.Domain.Invoice;
using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.CompanyTests;

public sealed class CompanyTests
{
    [Fact]
    public void Company_CreateNew_SetsValues()
    {
        var email = CompanyEmail.CreateNew("company@company.com");
        var phone = CompanyPhone.CreateNew("123-456-7890");
        var address = CompanyAddress.CreateNew("123 street", null, "city", "state", "zip", "US");
        var client = Client.CreateNew(
            new ClientName("Client"),
            new ClientContactName("The Client"),
            ClientEmail.CreateNew("client@mail.com"),
            ClientPhone.CreateNew("123-456-7890"),
            ClientAddress.CreateNew("321 street", null, "city", "state", "zip", "US"));
        var company = Company.CreateNew(
            new CompanyName("Company LLC"),
            new CompanyContactName("The Company"),
            email,
            phone,
            address,
            client);
        
        company.Address.ShouldBeSameAs(address);
        company.Client.ShouldBeSameAs(client);
        company.Email.ShouldBeSameAs(email);
        company.Phone.ShouldBeSameAs(phone);
        company.Name.Value.ShouldBe("Company LLC");
        company.ContactName.Value.ShouldBe("The Company");
    }

    [Fact]
    public void Company_CreateInvoiceForClient_ReturnsInvoice()
    {
        var company = Company.CreateNew(
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
        var systemClock = ManualSystemClock.CreateNew(DateTime.UtcNow);
        var terms = InvoiceStandardTerms.CreateNewDaysFromNow(systemClock, 10);
        var invoice = company.CreateInvoiceForClient(
            company.Client, 
            InvoiceDueDate.CreateNew(terms), 
            InvoiceNumber.CreateNew("abc123"));
        invoice.Client.ShouldBe(company.Client);
        invoice.Company.ShouldBe(company);
        invoice.DueDate.Value.ShouldBe(systemClock.Now.AddDays(10));
    }
}