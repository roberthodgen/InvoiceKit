namespace InvoiceKit.Domain.Company;

public sealed record CompanyName
{
    public string Value { get; }
    
    public CompanyName(string input)
    {
        Value = input;
    }

    public override string ToString()
    {
        return Value;
    }
}