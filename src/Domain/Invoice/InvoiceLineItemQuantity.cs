namespace InvoiceKit.Domain.Invoice;

public sealed record InvoiceLineItemQuantity
{
    public decimal Value { get; }
    
    private InvoiceLineItemQuantity(decimal value)
    {
        Value = value;
    }

    public static InvoiceLineItemQuantity CreateNew(decimal input)
    {
        return new InvoiceLineItemQuantity(input);
    }
}