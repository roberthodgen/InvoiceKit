namespace InvoiceKit.Domain.Company;

public sealed record CompanyContactName
{
    public string Value { get; }
    
    public CompanyContactName(string input)
    {
        Value = input;   
    }

    public override string ToString()
    {
        return Value; 
    }
}