using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Domain.Client;

public sealed record ClientPhone : Phone
{
    private ClientPhone(string value) : base(value)
    {
        
    }

    public static ClientPhone CreateNew(string value)
    {
        return new ClientPhone(value);
    }
    
}