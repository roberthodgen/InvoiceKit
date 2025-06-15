namespace InvoiceKit.Domain;

/// <summary>
/// Tax rates may be assigned per invoice line item. This accommodates non-taxed items.
/// </summary>
public readonly record struct InvoiceLineItemTaxRate(decimal Percent);
