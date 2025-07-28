namespace InvoiceKit.Domain.Shared.Kernel;

/// <summary>
/// Singleton for only having one instance of a DateTime object from the ISystemClock.
/// </summary>
public sealed class CurrentSystemClock : ISystemClock
{
    public static CurrentSystemClock Instance { get; } = new ();
    
    public DateTime Now => DateTime.UtcNow;

    private CurrentSystemClock() { }
}
