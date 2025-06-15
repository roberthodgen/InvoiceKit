namespace InvoiceKit.Domain;

public sealed record InvoiceSubtotal
{
    public required string Type { get; init; }

    public required string Description { get; init; }

    public required decimal Amount { get; init; }
}
