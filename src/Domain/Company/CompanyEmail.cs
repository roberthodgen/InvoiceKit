using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Domain.Company;

public sealed record CompanyEmail : Email
{
    private CompanyEmail(string input) : base(input)
    {
        
    }

    public static CompanyEmail CreateNew(string input)
    {
        return new CompanyEmail(input);
    }
}