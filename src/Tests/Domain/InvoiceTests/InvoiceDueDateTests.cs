using InvoiceKit.Domain.Invoice;
using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.InvoiceTests;

public class InvoiceDueDateTests
{
    [Fact]
    public void DueDate_SetsValue()
    {
        var systemClock = ManualSystemClock.CreateNew(DateTime.UtcNow);
        var invoiceTerms = InvoiceStandardTerms.CreateNewDaysFromNow(systemClock, 10);
        var dueDate = InvoiceDueDate.CreateNew(invoiceTerms);
        dueDate.Value.Date.ShouldBe(systemClock.Now.Date.AddDays(10));
    }
    
    [Fact]
    public void DueDate_OnCreate_ThrowsExceptionForNonUtc()
    {
        var systemClock = ManualSystemClock.CreateNew(DateTime.Now);
        var invoiceTerms = InvoiceStandardTerms.CreateNewDaysFromNow(systemClock, 10);
        Assert.Throws<ArgumentException>(() => InvoiceDueDate.CreateNew(invoiceTerms));
    }
    
    [Fact]
    public void DueDate_ToString_ReturnsValue()
    {
        var dateTime = new DateTime(2025, 7, 15, 0, 0, 0, DateTimeKind.Utc);
        var systemClock = ManualSystemClock.CreateNew(dateTime);
        var invoiceTerms = InvoiceStandardTerms.CreateNewDaysFromNow(systemClock, 10);
        var dueDate = InvoiceDueDate.CreateNew(invoiceTerms);
        dueDate.ToString().ShouldBe("2025-07-25T00:00:00.0000000Z");
    }
}