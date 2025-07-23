namespace InvoiceKit.Domain.Shared.Kernel;

public abstract record Email
{
    public string Value { get; }

    protected Email(string input)
    {
        Value = input;   
    }

    public override string ToString()
    {
        return string.IsNullOrEmpty(Value) ? string.Empty : Value.ToLowerInvariant();
    }
    
}