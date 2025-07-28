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
    
    [Fact]
    public void ClientName_Constructor_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new ClientName(String.Empty));
    }
    
    [Fact]
    public void ClientName_ToString_ReturnsName()
    {
        var name = new ClientName("Name");
        name.ToString().ShouldBe("Name");
    }
}