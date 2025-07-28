namespace InvoiceKit.Domain.Shared.Kernel;

public abstract record Timestamp
{
    public DateTime Value { get; }

    protected Timestamp(DateTime value)
    {
        if (value.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException("Timestamp must be in UTC."); 
        }
        Value = value;  
    }

    /// <summary>
    /// Returns the Value field DateTime object into a string format based on current culture.
    /// </summary>
    /// <remarks> Could throw an exception if the culture is not set. </remarks>
    /// <returns>Formatted short date.</returns>
    public sealed override string ToString()
    {
        return Value.ToString("d");
    }
}