namespace InvoiceKit.Domain.Invoice;

public sealed class InvoiceFixedDueDate : IInvoiceTerms
{
    public DateTime GetDueDate { get; }

    private InvoiceFixedDueDate(DateTime dueDate)
    {
        GetDueDate = dueDate;
    }

    public static InvoiceFixedDueDate CreateNewFromDateOnly(DateOnly fixedDate)
    {
        return new InvoiceFixedDueDate(fixedDate.ToDateTime(TimeOnly.MaxValue, DateTimeKind.Utc));
    }
}
