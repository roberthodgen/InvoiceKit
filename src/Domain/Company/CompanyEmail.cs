using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Domain.Company;

public sealed record CompanyEmail : Email
{
    private CompanyEmail(string value) : base(value)
    {
        
    }

    public static CompanyEmail CreateNew(string value)
    {
        return new CompanyEmail(value);
    }
}