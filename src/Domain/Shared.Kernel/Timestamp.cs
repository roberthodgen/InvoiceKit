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
    /// Returns the Value field DateTime object into a string format.
    /// </summary>
    /// <returns>ISO 8601 format yyyy-MM-ddTHH:mm:ss.fffffffK</returns>
    public override string ToString()
    {
        return Value.ToString("o");
    }
    
    // Todo: More overloads for the toString methods for different date formats
    
}