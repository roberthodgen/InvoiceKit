using InvoiceKit.Domain.Invoice;
using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.Shared.Kernel.Tests;

public class TimestampTests
{
    [Fact]
    public void Timestamp_Now_ReturnsUtcNow()
    {
        
    }

    [Fact]
    public void Timestamp_ToString_ReturnsFormattedString()
    {
        var dateTime = new DateTime(2025, 7, 15, 0, 0, 0, DateTimeKind.Utc);
        var systemClock = ManualSystemClock.CreateNew(dateTime);
        var invoiceTerms = InvoiceStandardTerms.CreateNewDaysFromNow(systemClock, 10);
        var dueDate = InvoiceDueDate.CreateNew(invoiceTerms);
        dueDate.ToString().ShouldBe("2025-07-25T00:00:00.0000000Z");
    }

    [Fact]
    public void Timestamp_Constructor_ReturnsTimestamp()
    {
        var dateTime = new DateTime(2025, 7, 15, 0, 0, 0, DateTimeKind.Utc);
        var systemClock = ManualSystemClock.CreateNew(dateTime);
        var invoiceTerms = InvoiceStandardTerms.CreateNewDaysFromNow(systemClock, 10);
        var dueDate = InvoiceDueDate.CreateNew(invoiceTerms);
        dueDate.Value.ShouldBe(dateTime.AddDays(10));
    }
    
    [Fact]
    public void Timestamp_Constructor_ThrowsExceptionForNonUtc()
    {
        var systemClock = ManualSystemClock.CreateNew(DateTime.Now);
        var invoiceTerms = InvoiceStandardTerms.CreateNewDaysFromNow(systemClock, 10);
        Assert.Throws<ArgumentException>(() => InvoiceDueDate.CreateNew(invoiceTerms));
    }
}