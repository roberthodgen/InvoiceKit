namespace InvoiceKit.Domain.Shared.Kernel;

public abstract record Email
{
    public string Value { get; }

    protected Email(string value)
    {
        Value = value;   
    }

    public sealed override string ToString()
    {
        return string.IsNullOrEmpty(Value) ? string.Empty : Value.ToLowerInvariant();
    }
    
}