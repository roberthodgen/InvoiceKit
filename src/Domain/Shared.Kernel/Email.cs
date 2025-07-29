namespace InvoiceKit.Domain.Shared.Kernel;

public abstract record Email
{
    public string Value { get; }

    protected Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
        {
            throw new ArgumentException("Email is required.", nameof(value));
        }
        Value = value;   
    }

    public sealed override string ToString()
    {
        return Value.ToLowerInvariant();
    }
}
