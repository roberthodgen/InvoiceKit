using InvoiceKit.Domain.Company;

namespace InvoiceKit.Tests.Domain.CompanyTests;

public sealed class CompanyNameTests
{
    [Fact]
    public void CompanyName_Constructor_SetsValue()
    {
        var name = new CompanyName("Company Name");
        name.Value.ShouldBe("Company Name");
    }
    
    [Fact]
    public void CompanyName_Constructor_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new CompanyName(String.Empty));
    }
    
    [Fact]
    public void CompanyName_ToString_ReturnsValue()
    {
        var name = new CompanyName("Company Name");
        name.ToString().ShouldBe("Company Name");
    }
}