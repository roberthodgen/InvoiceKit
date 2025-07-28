using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Domain.Invoice;

public sealed record InvoiceLineItemSubTotal : AmountOfMoney
{
    private InvoiceLineItemSubTotal(decimal value) : base(value)
    {
    }

    internal static InvoiceLineItemSubTotal CreateNew(decimal input)
    {
        return new InvoiceLineItemSubTotal(input);  
    }
}