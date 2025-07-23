using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Domain.Invoice;

public sealed record InvoiceLineItemSubTotal : AmountOfMoney
{
    private InvoiceLineItemSubTotal(decimal input) : base(input)
    {
    }

    internal static InvoiceLineItemSubTotal CreateNew(decimal input)
    {
        return new InvoiceLineItemSubTotal(input);  
    }
}