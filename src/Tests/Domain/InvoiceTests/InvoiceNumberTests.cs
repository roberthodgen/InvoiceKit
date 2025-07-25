using InvoiceKit.Domain.Invoice;

namespace InvoiceKit.Tests.Domain.InvoiceTests;

public sealed class InvoiceNumberTests
{
    [Fact]
    public void InvoiceNumber_CreateNew_SetsValue()
    {
        var number = InvoiceNumber.CreateNew("abc123");
        number.Value.ShouldBe("abc123");
    }
    
    [Fact]
    public void InvoiceNumber_ToString_ReturnsValue()
    {
        var number = InvoiceNumber.CreateNew("abc123");
        number.ToString().ShouldBe("abc123");
    }
}