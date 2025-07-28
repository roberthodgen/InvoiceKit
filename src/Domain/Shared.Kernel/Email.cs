namespace InvoiceKit.Domain.Shared.Kernel;

public abstract record Email
{
    public string Value { get; }

    protected Email(string value)
    {
        if (string.IsNullOrEmpty(value) || !value.Contains('@'))
        {
            throw new ArgumentException("Email is required.", nameof(value));
        }
        Value = value;   
    }

    public sealed override string ToString()
    {
        return string.IsNullOrEmpty(Value) ? string.Empty : Value.ToLowerInvariant();
    }
    
}