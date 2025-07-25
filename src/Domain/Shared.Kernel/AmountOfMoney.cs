using System.Globalization;

namespace InvoiceKit.Domain.Shared.Kernel;

public abstract record AmountOfMoney
{
    public decimal Amount { get; }

    // Todo: Add currencies / currency converter
    protected AmountOfMoney(decimal amount)
    {
        Amount = amount;
    }
    
    /// <summary>A string formatted by the current location context of the user's computer.</summary>
    /// <exception cref="NullReferenceException">No location context is set.</exception>
    public sealed override string ToString()
    {
        return Amount.ToString("C2", CultureInfo.CurrentCulture);
    }

    public record ZeroAmountOfMoney : AmountOfMoney
    {
        private ZeroAmountOfMoney(decimal value) : base(value)
        {
            
        }

        public static ZeroAmountOfMoney GetInstance => new(0);
    }
}