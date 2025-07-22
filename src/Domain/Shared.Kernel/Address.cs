namespace InvoiceKit.Domain.Shared.Kernel;

public record Address
{
    public string Address1 { get; init; }
    public string? Address2 { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string ZipCode { get; init; }
    public string? Country { get; init; }

    protected Address(string address1, string? address2, string city, string state, string zipCode, string? country)
    {
        Address1 = address1;
        Address2 = address2 ?? string.Empty;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country ?? string.Empty;
    }

    public override string ToString()
    {
        return $"{Address1}, {Address2}, {City}, {State}, {ZipCode}, {Country}";
    }
}