using InvoiceKit.Domain.Client;
using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.ClientTests;

public sealed class ClientEmailTests
{
    [Fact]
    public void ClientEmail_IsA_EmailAddress()
    {
        typeof(ClientEmail).IsAssignableTo(typeof(Email)).ShouldBeTrue();
    }

    [Fact]
    public void ClientEmail_CreateNew_SetsValue()
    {
        var email = ClientEmail.CreateNew("name@mail.com");
        email.Value.ShouldBe("name@mail.com");
    }
}
