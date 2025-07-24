using InvoiceKit.Domain.Client;

namespace InvoiceKit.Tests.Domain.ClientTests;

public sealed class ClientNameTests
{
    [Fact]
    public void ClientName_Constructor_SetsName()
    {
        var name = new ClientName("Name");
        name.Value.ShouldBe("Name");
    }
}