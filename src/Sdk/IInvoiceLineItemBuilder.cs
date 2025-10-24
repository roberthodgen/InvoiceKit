namespace InvoiceKit.Sdk;

public interface IInvoiceLineItemBuilder
{
    /// <summary>
    /// Sets the line item's quantity.
    /// </summary>
    /// <remarks>
    /// Defaults to 1, if unspecified.
    /// </remarks>
    IInvoiceLineItemBuilder WithQuantity(int quantity = 1);

    /// <summary>
    /// Sets the line item's per-unit price.
    /// </summary>
    /// <remarks>
    /// This is the price per unit of the item. It will be multiplied with the quantity (<see cref="WithQuantity"/>) to
    /// calculate a total.
    /// </remarks>
    IInvoiceLineItemBuilder WithPerUnitPrice(decimal pricePerUnit);

    /// <summary>
    /// Sets the line item's description.
    /// </summary>
    IInvoiceLineItemBuilder WithDescription(string description);
}
