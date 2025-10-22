namespace InvoiceKit.Sdk;

using Domain.Invoice;

public sealed class InvoiceLineItemBuilder : IInvoiceLineItemBuilder
{
    private InvoiceLineItemDescription _description = InvoiceLineItemDescription.CreateNew("Item");

    private InvoiceLineItemPerUnitPrice? _perUnitPrice;

    private InvoiceLineItemQuantity _quantity = InvoiceLineItemQuantity.One;

    private List<string> _subtotals = [];

    /// <summary>
    /// Sets a line item's quantity. Default quantity of 1.
    /// </summary>
    public IInvoiceLineItemBuilder WithQuantity(int quantity)
    {
        _quantity = InvoiceLineItemQuantity.CreateNew(quantity);
        return this;
    }

    /// <summary>
    /// Sets a line item's price.
    /// </summary>
    public IInvoiceLineItemBuilder WithPerUnitPrice(decimal pricePerUnit)
    {
        _perUnitPrice = InvoiceLineItemPerUnitPrice.CreateNew(pricePerUnit);
        return this;
    }

    /// <summary>
    /// Sets a line item's description.
    /// </summary>
    public IInvoiceLineItemBuilder WithDescription(string description)
    {
        _description = InvoiceLineItemDescription.CreateNew(description);
        return this;
    }

    internal InvoiceLineItem Build()
    {
        if (_perUnitPrice is null)
        {
            throw new Exception("PerUnitPrice must be set..");
        }
        return InvoiceLineItem.CreateNew(_description, _perUnitPrice, _quantity);
    }
}
