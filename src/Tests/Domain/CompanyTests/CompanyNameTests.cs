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
    
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("\t\t")]
    public void CompanyName_Constructor_ThrowsException(string value)
    {
        Should.Throw<ArgumentException>(() => new CompanyName(value));
    }
    
    [Fact]
    public void CompanyName_ToString_ReturnsValue()
    {
        var name = new CompanyName("Company Name");
        name.ToString().ShouldBe("Company Name");
    }
}
