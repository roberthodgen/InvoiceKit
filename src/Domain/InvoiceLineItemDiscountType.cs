namespace InvoiceKit.Domain;

/// <summary>
/// The type of discount applied to a line item.
/// </summary>
public readonly record struct InvoiceLineItemDiscountType
{
    /// <summary>
    /// A fixed dollar amount is always applied.
    /// </summary>
    public static readonly InvoiceLineItemDiscountType Fixed = new ("Fixed");

    /// <summary>
    /// A percentage of the subtotal is applied.
    /// </summary>
    public static readonly InvoiceLineItemDiscountType Percentage = new ("Percentage");

    public string Value { get; }

    private InvoiceLineItemDiscountType(string value)
    {
        Value = value;
    }
}
