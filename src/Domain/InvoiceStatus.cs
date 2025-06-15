namespace InvoiceKit.Domain;

/// <summary>
/// The status of an invoice, i.e.: whether is is paid or not.
/// </summary>
public readonly record struct InvoiceStatus
{
    public static readonly InvoiceStatus Open = new ("Open");

    public static readonly InvoiceStatus Paid = new ("Paid");

    public string Value { get; }

    private InvoiceStatus(string value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }
}
