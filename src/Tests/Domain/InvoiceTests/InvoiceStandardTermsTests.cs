using InvoiceKit.Domain.Invoice;
using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.InvoiceTests;

public class InvoiceStandardTermsTests
{
    [Fact]
    public void Terms_SetsValue()
    {
        var systemClock = ManualSystemClock.CreateNew(DateTime.UtcNow);
        var terms = InvoiceStandardTerms.CreateNewDaysFromNow(systemClock, 10);
        terms.GetDueDate.ShouldBe(systemClock.Now.AddDays(10));
        terms.Days.ShouldBe(10);
    }
}