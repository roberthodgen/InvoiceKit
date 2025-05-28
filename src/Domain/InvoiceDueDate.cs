namespace InvoiceKit.Domain;

public sealed record InvoiceDueDate
{
    public InvoiceDueDateTerms Terms { get; }

    public DateOnly Date { get; }

    internal InvoiceDueDate(InvoiceDueDateTerms terms, DateOnly date)
    {
        Terms = terms;
        Date = date;
    }
}
