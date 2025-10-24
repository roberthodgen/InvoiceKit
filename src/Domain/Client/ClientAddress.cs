namespace InvoiceKit.Domain.Client;

using Shared.Kernel;

public sealed record ClientAddress : AddressBase
{
    private ClientAddress(
        string address1, 
        string? address2, 
        string city, 
        string state, 
        string zipCode, 
        string? country) 
        : base(address1, address2, city, state, zipCode, country) { }

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
