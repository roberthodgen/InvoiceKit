namespace InvoiceKit.Domain.Invoice;

using Shared.Kernel;

public sealed record InvoiceLineItemPerUnitPrice : AmountOfMoney
{
    private InvoiceLineItemPerUnitPrice(decimal value) : base(value) { }

    public static InvoiceLineItemPerUnitPrice CreateNew(decimal input)
    {
        return new InvoiceLineItemPerUnitPrice(input);   
    }
}
