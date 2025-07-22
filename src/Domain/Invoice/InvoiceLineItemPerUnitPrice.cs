using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Domain.Invoice;

public record InvoiceLineItemPerUnitPrice : AmountOfMoney
{
    private InvoiceLineItemPerUnitPrice(decimal input) : base(input)
    {
        
    }

    internal static InvoiceLineItemPerUnitPrice CreateNew(decimal input)
    {
        return new InvoiceLineItemPerUnitPrice(input);   
    }
}