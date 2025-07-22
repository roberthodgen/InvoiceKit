using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Domain.Client;

public record ClientAddress : Address
{
    private ClientAddress(
        string address1, 
        string? address2, 
        string city, 
        string state, 
        string zipCode, 
        string? country) 
        : base(address1, address2, city, state, zipCode, country)
    {
        
    }

    public static ClientAddress CreateNew(
        string address1, 
        string? address2, 
        string city, 
        string state, 
        string zipCode, 
        string? country)
    {
        return new ClientAddress(address1, address2, city, state, zipCode, country);
    }
    
}