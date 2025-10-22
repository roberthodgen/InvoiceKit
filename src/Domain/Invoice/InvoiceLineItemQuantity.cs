namespace InvoiceKit.Domain.Invoice;

public readonly record struct InvoiceLineItemQuantity
{
    public static readonly InvoiceLineItemQuantity One = CreateNew(1);

    public decimal Value { get; }

    private InvoiceLineItemQuantity(decimal value)
    {
        Value = value;
    }

    public static InvoiceLineItemQuantity CreateNew(decimal input)
    {
        return new InvoiceLineItemQuantity(input);
    }

    public override string ToString()
    {
        return $"{Value}";
    }
}
