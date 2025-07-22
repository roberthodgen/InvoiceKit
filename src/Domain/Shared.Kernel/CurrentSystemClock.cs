namespace InvoiceKit.Domain.Shared.Kernel;

/// <summary>
/// Singleton for only having one instance of a DateTime object from the ISystemClock.
/// </summary>
public class CurrentSystemClock : ISystemClock
{
    private static CurrentSystemClock? _instance;
    
    private static readonly object Lock = new();
    
    public static CurrentSystemClock Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (Lock)
                {
                    _instance ??= new CurrentSystemClock();
                }
            }
            return _instance;
        }
    }
    
    private CurrentSystemClock() { }
}