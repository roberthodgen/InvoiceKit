namespace InvoiceKit.Domain.Invoice;

public sealed record InvoiceNumber
{
    public string Value { get; }

    private InvoiceNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Invoice number is required.", nameof(value));
        }

        Value = value;
    }

    public static InvoiceNumber CreateNew(string value)
    {
        return new InvoiceNumber(value);
    }

    public override string ToString()
    {
        return Value;
    }
}
