using InvoiceKit.Domain.Invoice;

namespace InvoiceKit.Tests.Domain.InvoiceTests;

public sealed class InvoiceLineItemQuantityTests
{
    [Fact]
    public void InvoiceLineItemQuantity_One_Exists()
    {
        InvoiceLineItemQuantity.One.Value.ShouldBe(1);
        InvoiceLineItemQuantity.One.ShouldBeAssignableTo<InvoiceLineItemQuantity>();
    }

    [Fact]
    public void InvoiceLineItemQuantity_CreateNew_SetsValue()
    {
        var quantity = InvoiceLineItemQuantity.CreateNew(10);
        quantity.Value.ShouldBe(10);
    }
}
