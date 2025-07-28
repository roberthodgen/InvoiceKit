namespace InvoiceKit.Domain.Shared.Kernel;

public interface ISystemClock
{
    /// <remarks>
    /// DateTime that is always in UTC.
    /// </remarks>>
    DateTime Now { get; }
}
