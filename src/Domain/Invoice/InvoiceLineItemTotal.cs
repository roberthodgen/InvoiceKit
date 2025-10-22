namespace InvoiceKit.Domain.Invoice;

using Shared.Kernel;

public sealed record InvoiceLineItemTotal : AmountOfMoney
{
    private InvoiceLineItemTotal(decimal value)
        : base(value)
    {
    }

    internal static InvoiceLineItemTotal CreateNew(decimal input)
    {
        return new InvoiceLineItemTotal(input);
    }
}
