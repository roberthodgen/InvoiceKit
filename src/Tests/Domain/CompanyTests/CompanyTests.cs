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
        var company = Company.CreateNew(new CompanyName("Company LLC"));
        company.Name.Value.ShouldBe("Company LLC");
    }

    [Fact]
    public void Company_SetValue_SetsValues()
    {
        var company = Company.CreateNew(new CompanyName("Company LLC"));
        company.SetContactName(new CompanyContactName("Contact Name"));
        company.SetEmail(CompanyEmail.CreateNew("finance@company.com"));
        company.SetPhone(CompanyPhone.CreateNew("123-456-7890"));
        company.SetAddress(CompanyAddress.CreateNew("123 street", null, "city", "state", "zip", "country"));

        company.ContactName?.Value.ShouldBe("Contact Name");
        company.Email?.Value.ShouldBe("finance@company.com");
        company.Phone?.Value.ShouldBe("123-456-7890");
        company.Address?.Address1.ShouldBe("123 street");
        company.Address?.Address2.ShouldBeNull();
        company.Address?.City.ShouldBe("city");
        company.Address?.State.ShouldBe("state");
        company.Address?.ZipCode.ShouldBe("zip");
        company.Address?.Country.ShouldBe("country");
    }

    [Fact]
    public void Company_CreateInvoiceForClient_ReturnsInvoice()
    {
        var company = Company.CreateNew(new CompanyName("Company LLC"));
        var client = Client.CreateNew(new ClientName("Client"));
        var systemClock = ManualSystemClock.CreateNew(DateTime.UtcNow);
        var terms = InvoiceStandardTerms.CreateNewDaysFromNow(systemClock, 10);
        var invoice = company.CreateInvoiceForClient(
            client,
            InvoiceDueDate.CreateNew(terms),
            InvoiceNumber.CreateNew("abc123"),
            null);

        invoice.Client.ShouldBe(client);
        invoice.Company.ShouldBe(company);
        invoice.DueDate.Value.Date.ShouldBe(systemClock.Now.Date.AddDays(10));
    }

    [Fact]
    public void Company_CreateInvoiceWithoutClient_ReturnsInvoice()
    {
        var company = Company.CreateNew(new CompanyName("Company LLC"));
        var systemClock = ManualSystemClock.CreateNew(DateTime.UtcNow);
        var terms = InvoiceStandardTerms.CreateNewDaysFromNow(systemClock, 10);
        var invoice = company.CreateInvoiceWithoutClient(
            InvoiceDueDate.CreateNew(terms),
            InvoiceNumber.CreateNew("abc123"),
            null);

        invoice.Company.ShouldBe(company);
        invoice.Client.ShouldBeNull();
        invoice.DueDate.Value.Date.ShouldBe(systemClock.Now.Date.AddDays(10));
    }

    [Fact]
    public void Company_ToString_ReturnsName()
    {
        var company = Company.CreateNew(new CompanyName("Company LLC"));
        company.SetContactName(new CompanyContactName("Contact Name"));
        company.SetAddress(CompanyAddress.CreateNew("123 street", "apartment", "city", "state", "zip", "country"));
        company.SetPhone(CompanyPhone.CreateNew("123-456-7890"));
        company.SetEmail(CompanyEmail.CreateNew("finance@company.com"));
        company.ToString().ShouldBe("Company LLC\nContact Name\nfinance@company.com\n123-456-7890\n123 street, apartment, city, state, zip, country");
    }
}
