namespace InvoiceKit.Domain.Client;

using Shared.Kernel;

public sealed record ClientEmail : Email
{
    private ClientEmail(string value) : base(value) { }

    public static ClientEmail CreateNew(string value)
    {
        return new ClientEmail(value);
    }
}
