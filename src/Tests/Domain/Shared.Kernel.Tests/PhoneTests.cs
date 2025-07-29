using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.Shared.Kernel.Tests;

public sealed class PhoneTests
{
    private sealed record PhoneTest : Phone
    {
        public PhoneTest(string input) : base(input)
        {
            
        }
    }
    
    [Fact]
    public void Phone_Constructor_SetsValue()
    {
        var phone = new PhoneTest("123456789");
        phone.Value.ShouldBe("123456789");
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("\t\t")]
    public void Phone_Constructor_NullOrEmpty(string value)
    {
        Should.Throw<ArgumentException>(() => new PhoneTest(value));
    }
    
    [Fact]
    public void Phone_ToString_ReturnsValue()
    {
        var phone = new PhoneTest("(123)456-7890");
        phone.ToString().ShouldBe("(123)456-7890");
    }
}
