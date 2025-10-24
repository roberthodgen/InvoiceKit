namespace InvoiceKit.Tests.Domain.InvoiceTests;

using InvoiceKit.Domain.Invoice;

public sealed class InvoiceFixedDueDateTests
{
    [Fact]
    public void InvoiceFixedDueDate_CreateNewFromDateOnly_SetsValue()
    {
        var dateOnly = DateOnly.FromDateTime(DateTime.UtcNow.Date);
        var terms = InvoiceFixedDueDate.CreateNewFromDateOnly(dateOnly);
        terms.GetDueDate.ShouldBe(dateOnly.ToDateTime(TimeOnly.MaxValue, DateTimeKind.Utc));
    }
}
