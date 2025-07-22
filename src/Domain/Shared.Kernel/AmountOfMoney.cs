using System.Globalization;

namespace InvoiceKit.Domain.Shared.Kernel;

public record AmountOfMoney
{
    public decimal Amount { get; }

    // Todo: Add currencies / currency converter
    protected AmountOfMoney(decimal amount)
    {
        Amount = amount;
    }
    
    /// <summary>A string formatted by the current location context of the user's computer.</summary>
    /// <exception cref="NullReferenceException">No location context is set.</exception>
    public override string ToString()
    {
        return Amount.ToString("C2", CultureInfo.CurrentCulture);
    }

    private record ZeroAmountOfMoney() : AmountOfMoney(0);
}