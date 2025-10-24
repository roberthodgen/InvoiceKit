namespace InvoiceKit.Sdk;

using Domain.Client;

public interface IClientBuilder
{
    /// <summary>
    /// Sets a client's address.
    /// </summary>
    IClientBuilder WithAddress(string address, string? address2, string city, string state, string zip, string? country);

    /// <summary>
    /// Sets a client's email address.
    /// </summary>
    IClientBuilder WithEmail(string email);

    /// <summary>
    /// Sets a client's phone number.
    /// </summary>
    IClientBuilder WithPhone(string phone);

    /// <summary>
    /// Sets a client's contact name.
    /// </summary>
    IClientBuilder WithContactName(string name);
}
