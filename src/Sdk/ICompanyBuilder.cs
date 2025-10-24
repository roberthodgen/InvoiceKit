namespace InvoiceKit.Sdk;

public interface ICompanyBuilder
{
    /// <summary>
    /// Sets a contact person's name.
    /// </summary>
    ICompanyBuilder WithContactName(string name);

    /// <summary>
    /// Sets a contact email.
    /// </summary>
    ICompanyBuilder WithEmail(string email);

    /// <summary>
    /// Sets a contact phone number.
    /// </summary>
    ICompanyBuilder WithPhone(string phone);

    /// <summary>
    /// Sets a company's address.
    /// </summary>
    ICompanyBuilder WithAddress(string address, string? address2, string city, string state, string zipCode, string? country);
}
