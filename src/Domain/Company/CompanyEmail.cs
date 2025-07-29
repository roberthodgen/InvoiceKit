namespace InvoiceKit.Domain.Company;

using Shared.Kernel;

public sealed record CompanyEmail : Email
{
    private CompanyEmail(string value) : base(value) { }

    public static CompanyEmail CreateNew(string value)
    {
        return new CompanyEmail(value);
    }
}
