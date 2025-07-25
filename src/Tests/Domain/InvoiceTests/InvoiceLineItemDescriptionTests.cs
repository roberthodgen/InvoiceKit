using InvoiceKit.Domain.Invoice;

namespace InvoiceKit.Tests.Domain.InvoiceTests;

public sealed class InvoiceLineItemDescriptionTests
{
    [Fact]
    public void Description_SetsValue()
    {
        var description = InvoiceLineItemDescription.CreateNew("This is a description");
        description.Value.ShouldBe("This is a description");
    }
    
    [Fact]
    public void Description_ToString_ReturnsValue()
    {
        var description = InvoiceLineItemDescription.CreateNew("This is a description");
        description.ToString().ShouldBe("This is a description");
    }
}