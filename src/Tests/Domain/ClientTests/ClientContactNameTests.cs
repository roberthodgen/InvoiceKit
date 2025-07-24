using InvoiceKit.Domain.Client;

namespace InvoiceKit.Tests.Domain.ClientTests;

public sealed class ClientContactNameTests
{
    [Fact]
    public void ClientContactName_Constructor_SetsValue()
    {
        var contactName = new ClientContactName("Name");
        contactName.Value.ShouldBe("Name");
    }
}