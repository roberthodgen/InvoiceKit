using System.Globalization;

namespace InvoiceKit.Domain.Shared.Kernel;

public abstract record AmountOfMoney
{
    public decimal Amount { get; }
    
    public static AmountOfMoney Zero => new ZeroAmountOfMoney();

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

    public sealed record ZeroAmountOfMoney : AmountOfMoney
    {
        internal ZeroAmountOfMoney() : base(0)
        {
            
        }

        public static ZeroAmountOfMoney GetInstance => new();
    }
}