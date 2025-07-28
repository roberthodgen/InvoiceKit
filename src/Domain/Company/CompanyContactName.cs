namespace InvoiceKit.Domain.Company;

public sealed record CompanyContactName
{
    public string Value { get; }
    
    public CompanyContactName(string value)
    {
        Value = value;   
    }

    public override string ToString()
    {
        return Value; 
    }
}