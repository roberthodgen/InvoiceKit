using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Domain.Invoice;

public sealed record InvoiceLineItemPerUnitPrice : AmountOfMoney
{
    private InvoiceLineItemPerUnitPrice(decimal input) : base(input)
    {
        
    }

    public static InvoiceLineItemPerUnitPrice CreateNew(decimal input)
    {
        return new InvoiceLineItemPerUnitPrice(input);   
    }
}