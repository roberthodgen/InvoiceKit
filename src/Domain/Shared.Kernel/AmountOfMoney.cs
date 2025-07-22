using System.Globalization;

namespace InvoiceKit.Domain.Shared.Kernel;

public abstract record AmountOfMoney
{
    public decimal Amount { get; init; }

    // Todo: Add currencies / currency converter
    protected AmountOfMoney(decimal amount)
    {
        Amount = amount;
    }
    
    /// <summary>A string formatted for US money by default. Change the country code for other currencies.</summary>
    /// <returns> 1000.000 => $1,000.00</returns>
    public override string ToString()
    {
        return Amount.ToString("C2", new CultureInfo("en-US"));
    }
}