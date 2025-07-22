namespace InvoiceKit.Domain.Shared.Kernel;

public interface ISystemClock
{
    /// <remarks>
    /// DateTime that is always in UTC.
    /// </remarks>>
    public DateTime Now => DateTime.UtcNow;
}