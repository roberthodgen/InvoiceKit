using InvoiceKit.Domain.Invoice;

namespace InvoiceKit.Tests.Domain.InvoiceTests;

public sealed class InvoiceNumberTests
{
    [Fact]
    public void Number_SetsValue()
    {
        var number = InvoiceNumber.CreateNew("abc123");
        number.Value.ShouldBe("abc123");
    }
    
    [Fact]
    public void Number_ToString_ReturnsValue()
    {
        var number = InvoiceNumber.CreateNew("abc123");
        number.ToString().ShouldBe("abc123");
    }
}