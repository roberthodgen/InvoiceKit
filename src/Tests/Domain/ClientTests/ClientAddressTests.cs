using InvoiceKit.Domain.Client;
using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.ClientTests;

public sealed class ClientAddressTests
{
    [Fact]
    public void ClientAddress_IsA_Address()
    {
        var clientAddress = ClientAddress.CreateNew("123 street", "apartment", "city", "state", "zip", "US");
        Assert.IsAssignableFrom<Address>(clientAddress);
    }
    
    [Fact]
    public void ClientAddress_CreateNew_SetsValues()
    {
        var clientAddress = ClientAddress.CreateNew("123 street", "apartment", "city", "state", "zip", "US");
        clientAddress.Address1.ShouldBe("123 street");
        clientAddress.Address2.ShouldBe("apartment");
        clientAddress.City.ShouldBe("city");
        clientAddress.State.ShouldBe("state");
        clientAddress.ZipCode.ShouldBe("zip");
    }

    [Fact]
    public void ClientAddress_ToString_ReturnsFormattedString()
    {
        var clientAddress = ClientAddress.CreateNew("123 street", "apartment", "city", "state", "zip", "US");
        clientAddress.ToString().ShouldBe("123 street\napartment\ncity, state zip\nUS");   
    }
}