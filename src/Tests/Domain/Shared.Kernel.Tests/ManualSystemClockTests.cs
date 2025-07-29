using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.Shared.Kernel.Tests;

public class ManualSystemClockTests
{
    [Fact]
    public void ManualSystemClock_CreateNew_ReturnsFixedTime()
    {
        var fixedTime = ManualSystemClock.CreateNew(new DateTime(2022, 1, 1));
        fixedTime.Now.ShouldBe(new DateTime(2022, 1, 1));
    }
}