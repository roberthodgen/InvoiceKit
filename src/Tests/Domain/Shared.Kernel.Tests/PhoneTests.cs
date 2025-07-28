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
    
    [Fact]
    public void Phone_ToString_ReturnsFormatted()
    {
        var phone = new PhoneTest("123-456-7890");
        phone.ToString().ShouldBe("(123)456-7890");
    }
    
    [Fact]
    public void Phone_ToString_ReturnsUnformatted()
    {
        var phone = new PhoneTest("123-456-789000");
        phone.ToString().ShouldBe("123-456-789000");
    }

    [Fact]
    public void Phone_ToString_NullOrEmpty()
    {
        var phone = new PhoneTest(String.Empty);
        phone.ToString().ShouldBe(String.Empty);
    }
}