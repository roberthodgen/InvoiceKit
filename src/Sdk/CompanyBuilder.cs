namespace InvoiceKit.Sdk;

using Domain.Company;

public sealed class CompanyBuilder : ICompanyBuilder
{
    private readonly CompanyName _name;

    private CompanyContactName? _contactName;

    private CompanyAddress? _address;

    private CompanyEmail? _email;

    private CompanyPhone? _phone;

    internal CompanyBuilder(string name)
    {
        _name = new CompanyName(name);
    }

    public ICompanyBuilder WithContactName(string name)
    {
        _contactName = new CompanyContactName(name);
        return this;
    }

    public ICompanyBuilder WithEmail(string email)
    {
        _email = CompanyEmail.CreateNew(email);
        return this;
    }

    public ICompanyBuilder WithPhone(string phone)
    {
        _phone = CompanyPhone.CreateNew(phone);
        return this;
    }

    public ICompanyBuilder WithAddress(
        string address,
        string? address2,
        string city,
        string state,
        string zipCode,
        string? country)
    {
        _address = CompanyAddress.CreateNew(address, address2, city, state, zipCode, country);
        return this;
    }

    internal Company Build()
    {
        var company = Company.CreateNew(_name);

        if (_contactName is not null)
        {
            company.SetContactName(_contactName);
        }

        if (_address is not null)
        {
            company.SetAddress(_address);
        }

        if (_email is not null)
        {
            company.SetEmail(_email);
        }

        if (_phone is not null)
        {
            company.SetPhone(_phone);
        }

        return company;
    }
}
