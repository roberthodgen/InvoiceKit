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

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("\t\t")]
    public void ClientContactName_Constructor_ThrowsException(string value)
    {
        Should.Throw<ArgumentException>(() => new ClientContactName(value));
    }
    
    [Fact]
    public void ClientContactName_ToString_ReturnsValue()
    {
        var contactName = new ClientContactName("Name");
        contactName.ToString().ShouldBe("Name");
    }
}
