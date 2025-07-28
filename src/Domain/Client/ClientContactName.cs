namespace InvoiceKit.Domain.Client;

public sealed record ClientContactName
{
    public string Value { get; }
    
    public ClientContactName(string value)
    {
        Value = value;   
    }
    
    public override string ToString()
    {
        return Value;  
    }
    
}