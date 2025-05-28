namespace InvoiceKit.Domain;

public sealed record InvoiceLineItem
{
    public required string Description { get; init; }

    public required decimal Quantity { get; init; }

    public required decimal UnitPrice { get; init; }

    /// <summary>
    /// Optional tax rate to apply to the line item.
    /// </summary>
    /// <remarks>
    /// The tax rate is applied to the subtotal after the discount, if any, is applied.
    /// </remarks>
    public InvoiceLineItemTaxRate? TaxRate { get; init; }

    /// <summary>
    /// Optional discount to apply to the line item.
    /// </summary>
    public InvoiceLineItemDiscount? Discount { get; init; }

    public decimal Subtotal => throw new NotImplementedException(); // (Quantity * UnitPrice);

    /// <summary>
    /// Total tax applied to the line item.
    /// </summary>
    public decimal Tax => throw new NotImplementedException();

    /// <summary>
    /// Line item total including tax and discount.
    /// </summary>
    public decimal Total => throw new NotImplementedException();
}
