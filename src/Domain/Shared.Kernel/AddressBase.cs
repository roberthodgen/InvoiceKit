namespace InvoiceKit.Domain.Shared.Kernel;

using System.Text;

public abstract record AddressBase
{
    public string Address1 { get; }
    public string? Address2 { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }
    public string? Country { get; }

    protected AddressBase(
        string address1,
        string? address2,
        string city,
        string state,
        string zipCode,
        string? country)
    {
        Address1 = address1;
        Address2 = address2;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
    }

    public sealed override string ToString()
    {
        var fullAddress = new StringBuilder();
        fullAddress.Append(Address1);
        if (Address2 is not null)
        {
            fullAddress.Append(", " + Address2);
        }
        fullAddress.Append(", " + City);
        fullAddress.Append(", " + State);
        fullAddress.Append(", " + ZipCode);
        if (Country is not null)
        {
            fullAddress.Append(", " + Country);
        }
        return fullAddress.ToString();
    }
}
