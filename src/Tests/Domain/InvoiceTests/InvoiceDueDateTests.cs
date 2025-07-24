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
        dueDate.Value.ShouldBe(systemClock.Now.AddDays(10));
    }
}