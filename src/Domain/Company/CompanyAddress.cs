namespace InvoiceKit.Domain.Company;

using Shared.Kernel;

public sealed record CompanyAddress : Address
{
    private CompanyAddress(
        string address1, 
        string? address2, 
        string city, 
        string state, 
        string zipCode, 
        string? country) 
        : base(address1, address2, city, state, zipCode, country) { }

    public static CompanyAddress CreateNew(
        string address1,
        string? address2,
        string city,
        string state,
        string zipCode,
        string? country)
    {
        return new CompanyAddress(address1, address2, city, state, zipCode, country);   
    }
}
