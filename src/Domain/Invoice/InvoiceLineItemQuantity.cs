namespace InvoiceKit.Domain.Invoice;

public readonly record struct InvoiceLineItemQuantity
{
    public static readonly InvoiceLineItemQuantity Zero = CreateNew(0);

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
