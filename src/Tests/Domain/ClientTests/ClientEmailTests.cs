using InvoiceKit.Domain.Client;
using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.ClientTests;

public sealed class ClientEmailTests
{
    [Fact]
    public void ClientEmail_IsA_EmailAddress()
    {
        var email = ClientEmail.CreateNew("name@mail.com");
        Assert.IsAssignableFrom<Email>(email);
    }

    [Fact]
    public void ClientEmail_CreateNew_SetsValue()
    {
        var email = ClientEmail.CreateNew("name@mail.com");
        email.Value.ShouldBe("name@mail.com");
    }
}