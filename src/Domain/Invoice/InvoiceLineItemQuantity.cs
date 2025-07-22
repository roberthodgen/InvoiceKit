namespace InvoiceKit.Domain.Invoice;

public record InvoiceLineItemQuantity
{
    public decimal Value { get; }
    
    private InvoiceLineItemQuantity(decimal quantity)
    {
        Value = quantity;
    }

    internal static InvoiceLineItemQuantity CreateNew(decimal input)
    {
        return new InvoiceLineItemQuantity(input);
    }
}