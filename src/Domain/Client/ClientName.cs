namespace InvoiceKit.Domain.Client;

public sealed record ClientName
{
    public string Value { get; }

    public ClientName(string input)
    {
        Value = input;   
    }

    public override string ToString()
    {
        return Value;  
    }
    
}