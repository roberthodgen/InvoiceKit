namespace InvoiceKit.Domain.Invoice;

public sealed record InvoiceLineItemQuantity
{
    public decimal Value { get; }
    
    private InvoiceLineItemQuantity(decimal quantity)
    {
        Value = quantity;
    }

    public static InvoiceLineItemQuantity CreateNew(decimal input)
    {
        return new InvoiceLineItemQuantity(input);
    }
}