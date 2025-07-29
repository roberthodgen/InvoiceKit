namespace InvoiceKit.Domain.Invoice;

public sealed record InvoiceLineItemDescription
{
    public string Value { get; }
    
    private InvoiceLineItemDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Invoice line item description is required.", nameof(value));
        }
        Value = value;
    }

    public static InvoiceLineItemDescription CreateNew(string input)
    {
        return new InvoiceLineItemDescription(input);
    }

    public override string ToString()
    {
        return Value;
    }
}
