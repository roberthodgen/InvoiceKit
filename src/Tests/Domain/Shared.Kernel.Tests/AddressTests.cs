using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.Shared.Kernel.Tests;

public sealed class AddressTests
{
    private sealed record AddressTest : Address
    {
        public AddressTest(string address1, string? address2, string city, string state, string zipCode, string? country) : base(address1, address2, city, state, zipCode, country)
        {
        }
    }
    
    [Fact]
    public void Address_Constructor_SetsValues()
    {
        var address = new AddressTest("123 Main St", "Apt 1", "Anytown", "NY", "12345", "USA");
        address.Address1.ShouldBe("123 Main St");
        address.Address2.ShouldBe("Apt 1");
        address.City.ShouldBe("Anytown");
        address.State.ShouldBe("NY");
        address.ZipCode.ShouldBe("12345");
        address.Country.ShouldBe("USA");
    }
    
    [Fact]
    public void Address_ToString_ReturnsValues()
    {
        var address = new AddressTest("123 Main St", "Apt 1", "Anytown", "NY", "12345", "USA");
        address.ToString().ShouldBe("123 Main St\nApt 1\nAnytown, NY 12345\nUSA");
    }
}