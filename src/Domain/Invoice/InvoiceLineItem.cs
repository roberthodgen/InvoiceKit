using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Domain.Invoice;

public class InvoiceLineItem
{
    internal AmountOfMoney SubTotal => InvoiceLineItemSubTotal.CreateNew(PerUnitPrice.Amount * Quantity.Value);

    public InvoiceLineItemDescription Description { get; }

    public InvoiceLineItemPerUnitPrice PerUnitPrice { get; }

    public InvoiceLineItemQuantity Quantity { get; }

    private InvoiceLineItem(
        InvoiceLineItemDescription description,
        InvoiceLineItemPerUnitPrice perUnitPrice,
        InvoiceLineItemQuantity quantity)
    {
        Description = description;
        PerUnitPrice = perUnitPrice;
        Quantity = quantity;
    }

    internal static InvoiceLineItem CreateNew(
        InvoiceLineItemDescription description,
        InvoiceLineItemPerUnitPrice perUnitPrice, 
        InvoiceLineItemQuantity quantity)
    {
        return new InvoiceLineItem(description, perUnitPrice, quantity);
    }
}