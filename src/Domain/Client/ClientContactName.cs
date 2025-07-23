namespace InvoiceKit.Domain.Client;

public sealed record ClientContactName
{
    public string Value { get; }
    
    public ClientContactName(string input)
    {
        Value = input;   
    }
    
    public override string ToString()
    {
        return Value;  
    }
    
}