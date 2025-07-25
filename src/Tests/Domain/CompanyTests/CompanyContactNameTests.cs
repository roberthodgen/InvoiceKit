using InvoiceKit.Domain.Company;

namespace InvoiceKit.Tests.Domain.CompanyTests;

public sealed class CompanyContactNameTests
{
    [Fact]
    public void CompanyContactName_Constructor_SetsValue()
    {
        var contactName = new CompanyContactName("contact name");
        contactName.Value.ShouldBe("contact name");
    }
    
    [Fact]
    public void CompanyContactName_ToString_ReturnsValue()
    {
        var contactName = new CompanyContactName("contact name");
        contactName.ToString().ShouldBe("contact name");
    }
}