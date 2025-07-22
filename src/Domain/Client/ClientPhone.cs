using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Domain.Client;

public record ClientPhone : Phone
{
    private ClientPhone(string input) : base(input)
    {
        
    }

    public static ClientPhone CreateNew(string input)
    {
        return new ClientPhone(input);
    }
    
}