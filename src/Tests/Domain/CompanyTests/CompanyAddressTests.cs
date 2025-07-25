using InvoiceKit.Domain.Company;
using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.CompanyTests;

public sealed class CompanyAddressTests
{
    [Fact]
    public void CompanyAddress_IsA_Address()
    {
        var address = CompanyAddress.CreateNew("123 street", "apartment", "city", "state", "zip", "US");
        Assert.IsAssignableFrom<Address>(address);
    }
    
    [Fact]
    public void CompanyAddress_CreateNew_SetsValue()
    {
        var address = CompanyAddress.CreateNew("123 street", "apartment", "city", "state", "zip", "US");
        address.Address1.ShouldBe("123 street");
        address.Address2.ShouldBe("apartment");
        address.City.ShouldBe("city");
        address.State.ShouldBe("state");
        address.ZipCode.ShouldBe("zip");
        address.Country.ShouldBe("US");
    }
}