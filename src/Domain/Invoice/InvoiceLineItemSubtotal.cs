namespace InvoiceKit.Domain.Invoice;

using Shared.Kernel;

public sealed record InvoiceLineItemSubtotal : AmountOfMoney
{
    private InvoiceLineItemSubtotal(decimal value)
        : base(value)
    {
    }

    internal static InvoiceLineItemSubtotal CreateNew(decimal input)
    {
        return new InvoiceLineItemSubtotal(input);
    }
}
