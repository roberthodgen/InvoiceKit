using InvoiceKit.Domain.Client;

namespace InvoiceKit.Tests.Domain.ClientTests;

public sealed class ClientTests
{
    [Fact]
    public void Client_CreateNew_SetsValues()
    {
        var client = Client.CreateNew(new ClientName("Name"));
        client.Name.Value.ShouldBe("Name");
    }

    [Fact]
    public void Client_SetValues_SetsValues()
    {
        var client = Client.CreateNew(new ClientName("Name"));
        client.SetContactName(new ClientContactName("The Client"));
        client.SetEmail(ClientEmail.CreateNew("name@client.com"));
        client.SetPhone(ClientPhone.CreateNew("123-456-7890"));
        client.SetAddress(ClientAddress.CreateNew("123 street", "apartment", "city", "state", "zip", "US"));
        client.ContactName?.Value.ShouldBe("The Client");
        client.Email?.Value.ShouldBe("name@client.com");
        client.Phone?.Value.ShouldBe("123-456-7890");
        client.Address?.Address1.ShouldBe("123 street");
        client.Address?.Address2.ShouldBe("apartment");
        client.Address?.City.ShouldBe("city");
        client.Address?.State.ShouldBe("state");
        client.Address?.ZipCode.ShouldBe("zip");
        client.Address?.Country.ShouldBe("US");
    }

    [Fact]
    public void Client_ToString_ReturnsValue()
    {
        var client = Client.CreateNew(new ClientName("Client LLC"));
        client.SetContactName(new ClientContactName("Contact Name"));
        client.SetAddress(ClientAddress.CreateNew("123 street", "apartment", "city", "state", "zip", "country"));
        client.SetPhone(ClientPhone.CreateNew("123-456-7890"));
        client.SetEmail(ClientEmail.CreateNew("finance@client.com"));
        client.ToString().ShouldBe("Client LLC\nContact Name\nfinance@client.com\n123-456-7890\n123 street\napartment\ncity, state zip\ncountry");
    }
}
