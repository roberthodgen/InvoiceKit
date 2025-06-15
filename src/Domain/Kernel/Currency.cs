namespace InvoiceKit.Domain.Kernel;

public enum Currency
{
    /// <summary>
    /// United States Dollar
    /// </summary>
    /// <remarks>
    /// Formats currencies for the United States, e.g.: <c>$1,234.56</c>.
    /// </remarks>
    UnitedStatesDollar,

    /// <summary>
    /// The default when no other is specified. Uses the local culture to format currency strings.
    /// </summary>
    /// <remarks>
    /// If a currency you'd like to use isn't available on this <see cref="Currency"/> object utilize
    /// <see cref="Unknown"/> and ensure the runtime's culture is correctly configured.
    /// </remarks>
    Unknown,
}
