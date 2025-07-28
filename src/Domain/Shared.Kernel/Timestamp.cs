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
    /// <remarks> There are other ToString methods to choose from with different formats.</remarks>
    /// <returns>ISO 8601 format yyyy-MM-ddTHH:mm:ss.fffffffK</returns>
    public sealed override string ToString()
    {
        return Value.ToString("o");
    }

    /// <summary>
    /// Removes the time values and only displays the date in this format.
    /// </summary>
    /// <returns> MM/DD/YYYY </returns>
    public string ToMonthDayYearString()
    {
        return Value.ToString("MM/dd/yyyy");
    }

    /// <summary>
    /// Removes the time values and only displays the date in this format.
    /// </summary>
    /// <returns> yyyy-MM-dd </returns>
    public string ToYearMonthDayString()
    {
        return Value.ToString("yyyy/MM/dd");
    }

    /// <summary>
    /// Removes the time values and only displays the date in this format.
    /// </summary>
    /// <returns></returns>
    public string ToDayMonthYearString()
    {
        return Value.ToString("dd/MM/yyyy");
    }
    
}