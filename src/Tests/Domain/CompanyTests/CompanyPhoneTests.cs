using InvoiceKit.Domain.Company;
using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.CompanyTests;

public sealed class CompanyPhoneTests
{
    [Fact]
    public void CompanyPhone_IsA_Phone()
    {
        var phone = CompanyPhone.CreateNew("123-456-7890");
        Assert.IsAssignableFrom<Phone>(phone);
    }
    
    [Fact]
    public void CompanyPhone_CreateNew_SetsValues()
    {
        var phone = CompanyPhone.CreateNew("123-456-7890");
        phone.Value.ShouldBe("123-456-7890");
    }
    
    [Fact]
    public void CompanyPhone_ToString_ReturnsValue()
    {
        var phone = CompanyPhone.CreateNew("123-456-7890");
        phone.ToString().ShouldBe("(123)456-7890");
    }
}