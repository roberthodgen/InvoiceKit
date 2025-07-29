using InvoiceKit.Domain.Client;

namespace InvoiceKit.Tests.Domain.ClientTests;

public sealed class ClientTests
{
    [Fact]
    public void Client_CreateNew_SetsValues()
    {
        var address = ClientAddress.CreateNew("123 street", "apartment", "city", "state", "zip", "US");
        var client = Client.CreateNew(
            new ClientName("Name"), 
            new ClientContactName("The Client"), 
            ClientEmail.CreateNew("Name@mail.com"), 
            ClientPhone.CreateNew("123-456-7890"),
            address);
        client.Name.Value.ShouldBe("Name");
        client.ContactName.Value.ShouldBe("The Client");
        client.Email.Value.ShouldBe("Name@mail.com");
        client.Phone.Value.ShouldBe("123-456-7890");
        client.Address.ShouldBeSameAs(address);
    }

    [Fact]
    public void Client_ToString_ReturnsValue()
    {
        var address = ClientAddress.CreateNew("123 street", "apartment", "city", "state", "zip", "US");
        var client = Client.CreateNew(
            new ClientName("Name"), 
            new ClientContactName("The Client"), 
            ClientEmail.CreateNew("Name@mail.com"), 
            ClientPhone.CreateNew("123-456-7890"),
            address);
        client.ToString().ShouldBe("Name");
    }
}