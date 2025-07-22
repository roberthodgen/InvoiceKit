namespace InvoiceKit.Domain.Shared.Kernel;

public record Timestamp
{
    public DateTime Value { get; }

    protected Timestamp(DateTime input)
    {
        Value = input.ToUniversalTime();  
    }

    /// <summary>
    /// Returns the Value field DateTime object into a string format.
    /// </summary>
    /// <returns>ISO 8601 format yyyy-MM-ddTHH:mm:ss.fffffffK</returns>
    public override string ToString()
    {
        return Value.ToString("o");
    }
    
}