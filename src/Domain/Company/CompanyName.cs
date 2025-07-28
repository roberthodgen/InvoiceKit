namespace InvoiceKit.Domain.Company;

public sealed record CompanyName
{
    public string Value { get; }
    
    public CompanyName(string value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }
}