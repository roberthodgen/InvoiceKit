using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Domain.Client;

public record ClientEmail : Email
{
    private ClientEmail(string input) : base(input)
    {
        
    }

    public static ClientEmail CreateNew(string input)
    {
        return new ClientEmail(input);
    }
}