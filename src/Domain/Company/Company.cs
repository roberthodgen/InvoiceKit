using InvoiceKit.Domain.Invoice;

namespace InvoiceKit.Domain.Company;

using Client;
using Invoice;

public sealed class Company
{
    public CompanyName Name { get; }
    
    public CompanyContactName ContactName { get; }
    
    public CompanyEmail Email { get; }
    
    public CompanyPhone Phone { get; }
    
    public CompanyAddress Address { get; }
    
    public Client Client { get; }

    private Company(
        CompanyName name, 
        CompanyContactName contactName, 
        CompanyEmail email, 
        CompanyPhone phone, 
        CompanyAddress address,
        Client client)
    {
        Name = name;
        ContactName = contactName;
        Email = email;
        Phone = phone;
        Address = address;
        Client = client;
    }

    public static Company CreateNew(
        CompanyName name, 
        CompanyContactName contactName, 
        CompanyEmail email, 
        CompanyPhone phone, 
        CompanyAddress address,
        Client client)
    {
        return new Company(name, contactName, email, phone, address, client);
    }

    public Invoice CreateInvoiceForClient(Client client, InvoiceDueDate dueDate, InvoiceNumber invoiceNumber)
    {
        return Invoice.CreateNew(invoiceNumber, dueDate, client, this);   
    }
    
    public sealed override string ToString()
    {
        return Name.ToString();
    }
}