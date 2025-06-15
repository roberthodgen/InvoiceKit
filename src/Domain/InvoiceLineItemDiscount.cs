namespace InvoiceKit.Domain;

using Kernel;

/// <summary>
/// Discount applied to a line item.
/// </summary>
public sealed record InvoiceLineItemDiscount
{
    public InvoiceLineItemDiscountType Type { get; }

    /// <summary>
    /// Description to show on the invoice for this discount.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// The amount of money this discount is worth, if <see cref="Type"/> is
    /// <see cref="InvoiceLineItemDiscountType.Fixed"/>.
    /// </summary>
    public AmountOfMoney? FixedAmount { get; }

    /// <summary>
    /// The percentage this discount is worth, if <see cref="Type"/> is
    /// <see cref="InvoiceLineItemDiscountType.Percentage"/>.
    /// </summary>
    public decimal? Percentage { get; }

    private InvoiceLineItemDiscount(string description, AmountOfMoney fixedAmount)
    {
        Type = InvoiceLineItemDiscountType.Fixed;
        Description = description;
        FixedAmount = fixedAmount;
    }

    private InvoiceLineItemDiscount(string description, decimal percentage)
    {
        Type = InvoiceLineItemDiscountType.Percentage;
        Description = description;
        Percentage = percentage;
    }

    /// <summary>
    /// Creates a new fixed amount discount.
    /// </summary>
    /// <param name="description">Description to show on the invoice for this discount.</param>
    /// <param name="amount">The amount of money this discount is worth.</param>
    public static InvoiceLineItemDiscount CreateNewFixed(string description, AmountOfMoney amount) =>
        new (description, amount);

    /// <summary>
    /// Creates a new percentage discount.
    /// </summary>
    /// <param name="description">Description to show on the invoice for this discount.</param>
    /// <param name="percentage">The percentage this discount is worth.</param>
    public static InvoiceLineItemDiscount CreateNewPercentage(string description, decimal percentage) =>
        new (description, percentage);
}
