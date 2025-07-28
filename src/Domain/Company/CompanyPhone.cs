using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Domain.Company;

public sealed record CompanyPhone : Phone
{
    private CompanyPhone(string value) : base(value)
    {
        
    }

    public static CompanyPhone CreateNew(string value)
    {
        return new CompanyPhone(value);
    }
}