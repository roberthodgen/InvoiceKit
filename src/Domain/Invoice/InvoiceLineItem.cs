namespace InvoiceKit.Domain.Invoice;

using Shared.Kernel;

public sealed class InvoiceLineItem
{
    public AmountOfMoney Subtotal => InvoiceLineItemSubtotal.CreateNew(PerUnitPrice.Amount * Quantity.Value);

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
