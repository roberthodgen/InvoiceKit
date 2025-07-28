namespace InvoiceKit.Domain.Shared.Kernel;

public abstract record Phone
{
    public string Value { get; }

    protected Phone(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("Phone number is required.", nameof(value));
        }
        Value = value;   
    }
    
    public sealed override string ToString()
    {
        return Value;
    }
}