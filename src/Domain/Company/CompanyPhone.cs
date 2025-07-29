namespace InvoiceKit.Domain.Company;

using Shared.Kernel;

public sealed record CompanyPhone : Phone
{
    private CompanyPhone(string value) : base(value) { }

    public static CompanyPhone CreateNew(string value)
    {
        return new CompanyPhone(value);
    }
}
