namespace InvoiceKit.Domain.Client;

public class Client
{
    public ClientName Name { get; }
    
    public ClientContactName ContactName { get; }
    
    public ClientEmail Email { get; }
    
    public ClientPhone Phone { get; }
    
    public ClientAddress Address { get; }
    
    private Client(
        ClientName name, 
        ClientContactName contactName, 
        ClientEmail email, 
        ClientPhone phone, 
        ClientAddress address)
    {
        Name = name;
        ContactName = contactName;
        Email = email;
        Phone = phone;
        Address = address;
    }

    public static Client CreateNew(ClientName name, 
        ClientContactName contactName, 
        ClientEmail email, 
        ClientPhone phone, 
        ClientAddress address)
    {
        return new Client(name, contactName, email, phone, address);
    }

    // Todo: Fix formatting
    public override string ToString()
    {
        throw new NotImplementedException();
    }
}