namespace InvoiceKit.Sdk;

public interface IInvoiceLineItemBuilder
{
    /// <summary>
    /// Sets the line item's quantity.
    /// </summary>
    /// <remarks>
    /// Defaults to 1, if unspecified.
    /// </remarks>
    IInvoiceLineItemBuilder WithQuantity(int quantity);

    /// <summary>
    /// Sets the line item's per-unit price.
    /// </summary>
    /// <remarks>
    /// This is the price per unit of the item. It will be multiplied with the quantity (<see cref="WithQuantity"/>) to
    /// calculate a total.
    /// </remarks>
    IInvoiceLineItemBuilder WithPerUnitPrice(decimal pricePerUnit);

    /// <summary>
    /// Assigns the invoice line item to a subtotal.
    /// </summary>
    /// <remarks>
    /// Omitting this on a line item will simply exclude this from a subtotal.
    /// </remarks>
    IInvoiceCompanyBuilder WithSubtotal(string subtotal);
}
