namespace InvoiceKit.Domain.Kernel;

public readonly record struct Currency
{
    /// <summary>
    /// United States Dollar
    /// </summary>
    /// <remarks>
    /// Formats currencies for the United States, e.g.: <c>$1,234.56</c>.
    /// </remarks>
    public static Currency UnitedStatesDollar => new ("USD");

    public string Value { get; }

    private Currency(string value)
    {
        Value = value;
    }

    public AmountOfMoney Create(decimal amount)
    {
        return new AmountOfMoney(amount, this);
    }

    public override string ToString()
    {
        return Value;
    }
}
