using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Domain.Invoice;

/// <summary>
/// Describes the due date of the invoice.
/// </summary>
public sealed record InvoiceDueDate : Timestamp
{
    private InvoiceDueDate(DateTime value) 
        : base(value)
    {
        
    }

    public static InvoiceDueDate CreateNew(IInvoiceTerms terms)
    {
        return new InvoiceDueDate(terms.GetDueDate);
    }
}