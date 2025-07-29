namespace InvoiceKit.Domain.Shared.Kernel;

/// <summary>
/// Creates a system clock from the ISystemClock interface using a fixed time.
/// </summary>
/// <remarks>Useful for creating an invoice for a past date or future date.</remarks>
/// <returns>A fixed system clock.</returns>
public sealed class ManualSystemClock : ISystemClock
{
    private readonly DateTime _fixedTime;

    public DateTime Now => _fixedTime;
    
    private ManualSystemClock(DateTime fixedTime)
    {
        _fixedTime = fixedTime;   
    }

    public static ManualSystemClock CreateNew(DateTime fixedTime)
    {
        return new ManualSystemClock(fixedTime);
    }
}
