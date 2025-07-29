namespace InvoiceKit.Domain.Client;

using Shared.Kernel;

public sealed record ClientPhone : Phone
{
    private ClientPhone(string value) : base(value) { }

    public static ClientPhone CreateNew(string value)
    {
        return new ClientPhone(value);
    }
}
