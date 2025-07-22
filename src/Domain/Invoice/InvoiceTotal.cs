using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Domain.Invoice;

public record InvoiceTotal : AmountOfMoney
{
    private InvoiceTotal(decimal input) : base(input)
    {
    }

    private static InvoiceTotal CreateNew(decimal input)
    {
        return new InvoiceTotal(input);  
    }

    /// <summary>
    /// Gets the sum of all InvoiceLineItem Subtotals and returns a new InvoiceTotal object.
    /// </summary>
    /// <param name="invoiceLineItems">Readonly ICollection of InvoiceLineItems from the Invoice object.</param>
    /// <returns> New InvoiceTotal object</returns>
    internal static InvoiceTotal SumInvoiceLineItems(ICollection<InvoiceLineItem> invoiceLineItems) => 
        CreateNew(invoiceLineItems.Sum(x => x.SubTotal.Amount));
}