using InvoiceKit.Domain.Shared.Kernel;

namespace InvoiceKit.Tests.Domain.Shared.Kernel.Tests;

public sealed class CurrentSystemClockTests
{
    [Fact]
    public void CurrentSystemClock_IsFrom_ISystemClock()
    {
        var dateTime = CurrentSystemClock.Instance;
        Assert.IsAssignableFrom<ISystemClock>(dateTime);
    }

    [Fact]
    public void CurrentSystemClock_Instance_ReturnsSameInstance()
    {
        var instance = CurrentSystemClock.Instance;
        var instance2 = CurrentSystemClock.Instance;
        instance.ShouldBe(instance2);
    }
    
    [Fact]
    public void CurrentSystemClock_Now_ReturnsNow()
    {
        var dateTime = CurrentSystemClock.Instance.Now;
        dateTime.ShouldBeLessThanOrEqualTo(DateTime.UtcNow);
    }
}