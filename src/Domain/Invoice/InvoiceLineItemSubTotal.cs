namespace InvoiceKit.Domain.Invoice;

using Shared.Kernel;

public sealed record InvoiceLineItemSubTotal : AmountOfMoney
{
    private InvoiceLineItemSubTotal(decimal value) : base(value) { }

    internal static InvoiceLineItemSubTotal CreateNew(decimal input)
    {
        return new InvoiceLineItemSubTotal(input);  
    }
}
