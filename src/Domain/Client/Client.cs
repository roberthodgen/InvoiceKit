namespace InvoiceKit.Domain.Client;

using System.Text;

public sealed class Client
{
    public ClientName Name { get; }
    
    public ClientContactName? ContactName { get; private set; }
    
    public ClientEmail? Email { get; private set; }
    
    public ClientPhone? Phone { get; private set; }
    
    public ClientAddress? Address { get; private set; }
    
    private Client(ClientName name)
    {
        Name = name;
    }

    public static Client CreateNew(ClientName name)
    {
        return new Client(name);
    }

    public void SetContactName(ClientContactName contactName)
    {
        ContactName = contactName;
    }

    public void SetEmail(ClientEmail email)
    {
        Email = email;
    }

    public void SetPhone(ClientPhone phone)
    {
        Phone = phone;
    }

    public void SetAddress(ClientAddress address)
    {
        Address = address;
    }
    
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append(Name);
        if (ContactName is not null)
        {
            builder.Append("\n" + ContactName);
        }

        if (Email is not null)
        {
            builder.Append("\n" + Email);
        }

        if (Phone is not null)
        {
            builder.Append("\n" + Phone);
        }

        if (Address is not null)
        {
            builder.Append("\n" + Address);
        }
        return builder.ToString();
    }
}
