namespace InvoiceKit.Domain.Company;

public sealed class Company
{
    public CompanyName Name { get; }
    
    public CompanyContactName ContactName { get; }
    
    public CompanyEmail Email { get; }
    
    public CompanyPhone Phone { get; }
    
    public CompanyAddress Address { get; }

    private Company(
        CompanyName name, 
        CompanyContactName contactName, 
        CompanyEmail email, 
        CompanyPhone phone, 
        CompanyAddress address)
    {
        Name = name;
        ContactName = contactName;
        Email = email;
        Phone = phone;
        Address = address;   
    }

    public static Company CreateNew(
        CompanyName name, 
        CompanyContactName contactName, 
        CompanyEmail email, 
        CompanyPhone phone, 
        CompanyAddress address)
    {
        return new Company(name, contactName, email, phone, address);
    }
}