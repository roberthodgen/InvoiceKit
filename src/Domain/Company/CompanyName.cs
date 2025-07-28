namespace InvoiceKit.Domain.Company;

public sealed record CompanyName
{
    public string Value { get; }
    
    public CompanyName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("Company name is required.", nameof(value));
        }
        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }
}
