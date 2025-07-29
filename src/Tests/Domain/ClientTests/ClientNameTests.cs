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
    
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("\t\t")]
    public void ClientName_Constructor_ThrowsException(string value)
    {
        Should.Throw<ArgumentException>(() => new ClientName(value));
    }
    
    [Fact]
    public void ClientName_ToString_ReturnsName()
    {
        var name = new ClientName("Name");
        name.ToString().ShouldBe("Name");
    }
}
