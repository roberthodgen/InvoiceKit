namespace InvoiceKit.Domain.Client;

public sealed record ClientContactName
{
    public string Value { get; }
    
    public ClientContactName(string value)
    {
        if (String.IsNullOrEmpty(value))
        {
            throw new ArgumentException("Client contact name is required.", nameof(value));
        }
        Value = value;   
    }
    
    public override string ToString()
    {
        return Value;  
    }
}
