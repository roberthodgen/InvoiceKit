namespace InvoiceKit.Domain.Invoice;

using Shared.Kernel;

public sealed record InvoiceTotal : AmountOfMoney
{
    private InvoiceTotal(decimal value) : base(value) { }

    internal static InvoiceTotal CreateNew(decimal value)
    {
        return new InvoiceTotal(value);  
    }

    /// <summary>
    /// Gets the sum of all InvoiceLineItem Subtotals and returns a new InvoiceTotal object.
    /// </summary>
    /// <param name="invoiceLineItems">Readonly ICollection of InvoiceLineItems from the Invoice object.</param>
    /// <returns> New InvoiceTotal object</returns>
    internal static InvoiceTotal SumInvoiceLineItems(ICollection<InvoiceLineItem> invoiceLineItems) => 
        CreateNew(invoiceLineItems.Sum(x => x.SubTotal.Amount));
}
