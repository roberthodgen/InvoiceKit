namespace InvoiceKit.Domain.Client;

public sealed record ClientName
{
    public string Value { get; }

    public ClientName(string value)
    {
        Value = value;   
    }

    public override string ToString()
    {
        return Value;  
    }
    
}