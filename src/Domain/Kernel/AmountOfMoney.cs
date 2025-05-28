namespace InvoiceKit.Domain.Kernel;

public sealed record AmountOfMoney
{
    public decimal Amount { get; }

    public Currency Currency { get; }

    internal AmountOfMoney(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    /// <summary>
    /// Formats the amount of money for display in the correct format for its <see cref="Currency"/>.
    /// </summary>
    public override string ToString()
    {
        if (Currency == Currency.UnitedStatesDollar)
        {
            return $"{Amount:C,en-us}";
        }

        return $"{Amount} {Currency}";
    }
}
