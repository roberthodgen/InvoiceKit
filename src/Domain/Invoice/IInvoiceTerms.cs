namespace InvoiceKit.Domain.Invoice;

/// <summary>
/// Interface that allows a customizable due date selection.
/// </summary>
/// <remarks> Invoice standard terms 0 days, 30 days, 90 days, <see cref="InvoiceStandardTerms"/>
/// 1 month, next month, or end of month. </remarks>
public interface IInvoiceTerms
{ 
    DateTime GetDueDate { get; } 
}
