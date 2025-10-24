namespace InvoiceKit.Tests.Domain.InvoiceTests;

using InvoiceKit.Domain.Invoice;

public sealed class InvoiceLineItemDescriptionTests
{
    [Fact]
    public void InvoiceLineItemDescription_CreateNew_SetsValue()
    {
        var description = InvoiceLineItemDescription.CreateNew("This is a description");
        description.Value.ShouldBe("This is a description");
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("\t\t")]
    public void InvoiceLineItemDescription_CreateNew_ThrowsExceptionForEmptyValue(string value)
    {
        Should.Throw<ArgumentException>(() => InvoiceLineItemDescription.CreateNew(value));
    }
    
    [Fact]
    public void InvoiceLineItemDescription_ToString_ReturnsValue()
    {
        var description = InvoiceLineItemDescription.CreateNew("This is a description");
        description.ToString().ShouldBe("This is a description");
    }
}
