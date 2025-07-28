using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Domain.Invoice;

public sealed record InvoiceLineItemPerUnitPrice : AmountOfMoney
{
    private InvoiceLineItemPerUnitPrice(decimal value) : base(value)
    {
        
    }

    public static InvoiceLineItemPerUnitPrice CreateNew(decimal input)
    {
        return new InvoiceLineItemPerUnitPrice(input);   
    }
}