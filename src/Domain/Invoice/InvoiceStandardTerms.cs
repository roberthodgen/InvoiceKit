namespace InvoiceKit.Domain.Invoice;

using Shared.Kernel;

/// <summary>
/// The number of standard days to compute the invoice due date from.
/// </summary>
/// <remarks> A value of 0 means that it is due immediately. </remarks>
public sealed record InvoiceStandardTerms : IInvoiceTerms
{
    private readonly ISystemClock _systemClock;

    public int Days { get; }

    public DateTime GetDueDate => _systemClock.Now.AddDays(Days);

    private InvoiceStandardTerms(ISystemClock systemClock, int days)
    {
        if (days < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(days), "Days must be greater than or equal to 0.");
        }

        _systemClock = systemClock;
        Days = days;
    }

    public static InvoiceStandardTerms CreateNewDaysFromNow(ISystemClock systemClock, int days)
    {
        return new InvoiceStandardTerms(systemClock, days);
    }
}
