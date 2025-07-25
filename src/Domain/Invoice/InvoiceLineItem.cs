using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Domain.Invoice;

public sealed class InvoiceLineItem
{
    public AmountOfMoney SubTotal => InvoiceLineItemSubTotal.CreateNew(PerUnitPrice.Amount * Quantity.Value);

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

    public static InvoiceLineItem CreateNew(
        InvoiceLineItemDescription description,
        InvoiceLineItemPerUnitPrice perUnitPrice, 
        InvoiceLineItemQuantity quantity)
    {
        return new InvoiceLineItem(description, perUnitPrice, quantity);
    }
}