using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Domain.Company;

public record CompanyPhone : Phone
{
    private CompanyPhone(string input) : base(input)
    {
        
    }

    public static CompanyPhone CreateNew(string input)
    {
        return new CompanyPhone(input);
    }
}