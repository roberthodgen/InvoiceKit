using InvoiceKit.Domain.Invoice;

namespace InvoiceKit.Tests.Domain.InvoiceTests;

public sealed class InvoiceLineItemDescriptionTests
{
    [Fact]
    public void InvoiceLineItemDescription_CreateNew_SetsValue()
    {
        var description = InvoiceLineItemDescription.CreateNew("This is a description");
        description.Value.ShouldBe("This is a description");
    }

    [Fact]
    public void InvoiceLineItemDescription_CreateNew_ThrowsExceptionForEmptyValue()
    {
        Should.Throw<ArgumentException>(() => InvoiceLineItemDescription.CreateNew(String.Empty));
    }
    
    [Fact]
    public void InvoiceLineItemDescription_ToString_ReturnsValue()
    {
        var description = InvoiceLineItemDescription.CreateNew("This is a description");
        description.ToString().ShouldBe("This is a description");
    }
}
