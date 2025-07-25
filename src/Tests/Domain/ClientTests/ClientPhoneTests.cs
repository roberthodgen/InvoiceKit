using InvoiceKit.Domain.Client;
using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.ClientTests;

public sealed class ClientPhoneTests
{
    [Fact]
    public void ClientPhone_IsA_PhoneNumber()
    {
        var phoneNumber = ClientPhone.CreateNew("123-456-7890");
        Assert.IsAssignableFrom<Phone>(phoneNumber);
    }

    [Fact]
    public void ClientPhone_CreateNew_SetsValue()
    {
        var phoneNumber = ClientPhone.CreateNew("123-456-7890");
        phoneNumber.Value.ShouldBe("123-456-7890");
    }
    
    [Fact]
    public void ClientPhone_ToString_ReturnsValue()
    {
        var phoneNumber = ClientPhone.CreateNew("123-456-7890");
        phoneNumber.ToString().ShouldBe("(123)456-7890");
    }
}