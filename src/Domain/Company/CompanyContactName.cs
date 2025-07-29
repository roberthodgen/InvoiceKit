namespace InvoiceKit.Domain.Company;

public sealed record CompanyContactName
{
    public string Value { get; }
    
    public CompanyContactName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Company contact name is required.", nameof(value)); 
        }
        Value = value;   
    }

    public override string ToString()
    {
        return Value; 
    }
}
