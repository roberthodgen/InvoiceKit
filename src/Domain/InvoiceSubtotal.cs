namespace InvoiceKit.Domain;

public sealed record InvoiceSubtotal
{
    public string Type { get; }

    public string Description { get; }

    public decimal Amount { get; }
}
