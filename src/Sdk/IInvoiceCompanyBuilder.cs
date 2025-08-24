namespace InvoiceKit.Sdk;

public interface IInvoiceCompanyBuilder
{
    /// <summary>
    /// Sets a contact person's name.
    /// </summary>
    IInvoiceCompanyBuilder WithContactName(string name);

    /// <summary>
    /// Sets a contact email.
    /// </summary>
    IInvoiceCompanyBuilder WithContactEmail(string email);

    /// <summary>
    /// Sets a contact phone number.
    /// </summary>
    IInvoiceCompanyBuilder WithPhone(string phone);

    /// <summary>
    /// Sets a company's address.
    /// </summary>
    IInvoiceCompanyBuilder WithAddress(string address, string? address2, string city, string state, string zipCode);
}
