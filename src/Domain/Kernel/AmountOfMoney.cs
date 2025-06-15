namespace InvoiceKit.Domain.Kernel;

using System.Globalization;

public readonly record struct AmountOfMoney(decimal Amount, Currency Currency = Currency.Unknown)
{
    /// <summary>
    /// Formats the amount of money for display in the correct format for its <see cref="Currency"/>.
    /// </summary>
    public override string ToString() => Currency switch
    {
        Currency.UnitedStatesDollar => string.Format(new CultureInfo("en-US"), "{0:C}", Amount),
        Currency.Unknown => $"{Amount:C}",
        _ => throw new NotImplementedException("Currency not supported."),
    };
}
