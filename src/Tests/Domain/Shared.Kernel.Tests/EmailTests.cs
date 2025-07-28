using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.Shared.Kernel.Tests;

public sealed class EmailTests
{
    private sealed record EmailTest : Email
    {
        public EmailTest(string input) : base(input)
        {
        }
    }
    
    [Fact]
    public void Email_Constructor_SetsValue()
    {
        var email = new EmailTest("User@Mail.com");
        email.Value.ShouldBe("User@Mail.com");
    }
    
    [Fact]
    public void Email_Constructor_ThrowsException()
    {
        Should.Throw<ArgumentException>(() => new EmailTest(String.Empty));
    }

    [Fact]
    public void Email_ToString_ReturnsFormattedString()
    {
        var email = new EmailTest("User@Mail.com");
        email.ToString().ShouldBe("user@mail.com");
    }
}
