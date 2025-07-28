namespace InvoiceKit.Domain.Shared.Kernel;

public abstract record Address
{
    public string Address1 { get; }
    public string? Address2 { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }
    public string? Country { get; }

    protected Address(string address1, string? address2, string city, string state, string zipCode, string? country)
    {
        Address1 = address1;
        Address2 = address2 ?? string.Empty;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country ?? string.Empty;
    }

    public sealed override string ToString()
    {
        return $"""
                {Address1}
                {Address2}
                {City}, {State} {ZipCode}
                {Country}
                """;
    }
}
