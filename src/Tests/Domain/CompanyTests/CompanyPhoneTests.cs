using InvoiceKit.Domain.Company;
using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.CompanyTests;

public sealed class CompanyPhoneTests
{
    [Fact]
    public void CompanyPhone_IsA_Phone()
    {
        typeof(CompanyPhone).IsAssignableTo(typeof(Phone)).ShouldBeTrue();
    }
    
    [Fact]
    public void CompanyPhone_CreateNew_SetsValues()
    {
        var phone = CompanyPhone.CreateNew("123-456-7890");
        phone.Value.ShouldBe("123-456-7890");
    }
}
