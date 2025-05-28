namespace InvoiceKit.Domain;

public sealed class Invoice
{
    public required string Number { get; init; }

    public required DateTime IssuedAt { get; init; }

    public required InvoiceDueDate InvoiceDue { get; init; }

    public List<InvoiceSubtotal> Subtotals =>
        throw new NotImplementedException("Check line items for sub totals they belong to");

    public decimal Total => throw new NotImplementedException("TODO calculate");

    /// <summary>
    /// An optional note to include on the invoice.
    /// </summary>
    public string? Note { get; init; }

    public required InvoiceStatus Status { get; init; }

    public required Company CreatedBy { get; init; }

    public required Customer BilledTo { get; init; }

    public List<InvoiceLineItem> LineItems { get; } = [];

    public void AddLineItem(InvoiceLineItem lineItem)
    {
        LineItems.Add(lineItem);
    }
}
