using InvoiceKit.Domain.Company;
using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.CompanyTests;

public sealed class CompanyEmailTests
{
    [Fact]
    public void CompanyEmail_IsA_Email()
    {
        typeof(CompanyEmail).IsAssignableTo(typeof(Email)).ShouldBeTrue();
    }
    
    [Fact]
    public void CompanyEmail_CreateNew_SetsValue()
    {
        var email = CompanyEmail.CreateNew("company@mail.com");
        email.Value.ShouldBe("company@mail.com");
    }
}
