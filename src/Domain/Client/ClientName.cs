namespace InvoiceKit.Domain.Client;

public sealed record ClientName
{
    public string Value { get; }

    public ClientName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Client name is required.", nameof(value));   
        }
        Value = value;   
    }

    public override string ToString()
    {
        return Value;  
    }
}
