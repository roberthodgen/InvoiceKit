namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using SkiaSharp;

public class VerticalLayoutContextTests
{
    /// <summary>
    /// Points per inch.
    /// </summary>
    private const float Ppi = 72f;

    private static readonly SKRect UsLetter = SKRect.Create(new (8.5f * Ppi, 11f * Ppi));

    private readonly RootLayoutContext _root = new (UsLetter);

    [Fact]
    public void TryAllocate_DecreasesAvailable()
    {
        var context = _root.GetVerticalChildContext(UsLetter);
        context.TryAllocate(new SKSize(100, 100)).ShouldBeTrue();

        context.Available.Height.ShouldBe(UsLetter.Height - 100);
    }

    [Fact]
    public void TryAllocate_IncreasesAllocated()
    {
        var context = _root.GetVerticalChildContext(UsLetter);
        context.TryAllocate(new SKSize(100, 100)).ShouldBeTrue();

        context.Allocated.Size.Height.ShouldBe(100);
    }

    [Fact]
    public void Available_DecreasesAsChildrenAreCommitted()
    {
        var parent = _root.GetVerticalChildContext(UsLetter);
        var child = parent.GetVerticalChildContext(UsLetter);
        child.TryAllocate(new SKSize(100, 100)).ShouldBeTrue();

        // Act
        child.CommitChildContext();

        // Assert
        parent.Available.Size.Height.ShouldBe(UsLetter.Height - 100);
    }

    [Fact]
    public void Allocated_IncreasesAsChildrenAreCommitted()
    {
        var parent = _root.GetVerticalChildContext(UsLetter);
        var child = parent.GetVerticalChildContext(UsLetter);
        child.TryAllocate(new SKSize(100, 100)).ShouldBeTrue();

        // Act
        child.CommitChildContext();

        // Assert
        parent.Allocated.Size.Height.ShouldBe(100);
    }

    [Fact]
    public void Available_UnChangedUntilCommitted()
    {
        var parent = _root.GetVerticalChildContext(UsLetter);
        var child = parent.GetVerticalChildContext(UsLetter);
        child.TryAllocate(new SKSize(100, 100)).ShouldBeTrue();

        parent.Available.ShouldBe(UsLetter);
    }

    [Fact]
    public void Allocated_UnChangedUntilCommitted()
    {
        var parent = _root.GetVerticalChildContext(UsLetter);
        var child = parent.GetVerticalChildContext(UsLetter);
        child.TryAllocate(new SKSize(100, 100)).ShouldBeTrue();

        parent.Allocated.Size.Height.ShouldBe(0);
    }
}
