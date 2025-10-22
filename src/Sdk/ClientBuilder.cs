namespace InvoiceKit.Sdk;

using Domain.Client;

public sealed class ClientBuilder : IClientBuilder
{
    private readonly ClientName _name;

    private ClientContactName? _contactName;

    private ClientEmail? _email;

    private ClientPhone? _phone;

    private ClientAddress? _address;

    internal ClientBuilder(string name)
    {
        _name = new ClientName(name);
    }

    public IClientBuilder WithAddress(
        string address,
        string? address2,
        string city,
        string state,
        string zip,
        string? country)
    {
        _address = ClientAddress.CreateNew(address,  address2, city, state, zip, country);
        return this;
    }

    /// <summary>
    /// Adds an email to the client.
    /// </summary>
    public IClientBuilder WithEmail(string email)
    {
        _email = ClientEmail.CreateNew(email);
        return this;
    }

    /// <summary>
    /// Adds a phone number to the client.
    /// </summary>
    public IClientBuilder WithPhone(string phone)
    {
        _phone = ClientPhone.CreateNew(phone);
        return this;
    }

    /// <summary>
    /// Adds a contact name to the client.
    /// </summary>
    public IClientBuilder WithContactName(string name)
    {
        _contactName = new ClientContactName(name);
        return this;
    }

    internal Client Build()
    {
        var client = Client.CreateNew(_name);

        if (_contactName is not null)
        {
            client.SetContactName(_contactName);
        }

        if (_address is not null)
        {
            client.SetAddress(_address);
        }

        if (_email is not null)
        {
            client.SetEmail(_email);
        }

        if (_phone is not null)
        {
            client.SetPhone(_phone);
        }

        return client;
    }
}
