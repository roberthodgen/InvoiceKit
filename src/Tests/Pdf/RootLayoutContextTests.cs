namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using SkiaSharp;

public class RootLayoutContextTests
{
    /// <summary>
    /// Points per inch.
    /// </summary>
    private const float Ppi = 72f;

    private static readonly SKRect UsLetter = SKRect.Create(new (8.5f * Ppi, 11f * Ppi));

    [Fact]
    public void Available_DecreasesAsChildrenAreCommitted()
    {
        var root = new RootLayoutContext(UsLetter);
        root.Available.ShouldBe(UsLetter);
        root.Allocated.Size.Height.ShouldBe(0);

        var child = root.GetVerticalChildContext(UsLetter);
        child.TryAllocate(new SKSize(100, 100)).ShouldBeTrue();

        root.Available.ShouldBe(UsLetter);
        root.Allocated.Size.Height.ShouldBe(0);

        // Act
        child.CommitChildContext();

        // Assert
        root.Available.Size.Height.ShouldBe(UsLetter.Height - 100);
    }

    [Fact]
    public void Allocated_IncreasesAsChildrenAreCommitted()
    {
        var root = new RootLayoutContext(UsLetter);
        root.Available.ShouldBe(UsLetter);
        root.Allocated.Size.Height.ShouldBe(0);

        var child = root.GetVerticalChildContext(UsLetter);
        child.TryAllocate(new SKSize(100, 100)).ShouldBeTrue();

        root.Available.ShouldBe(UsLetter);
        root.Allocated.Size.Height.ShouldBe(0);

        // Act
        child.CommitChildContext();

        // Assert
        root.Allocated.Size.Height.ShouldBe(100);
    }

    [Fact]
    public void Available_UnChangedUntilCommitted()
    {
        var root = new RootLayoutContext(UsLetter);
        root.Available.ShouldBe(UsLetter);
        root.Allocated.Size.Height.ShouldBe(0);

        var child = root.GetVerticalChildContext(UsLetter);
        child.TryAllocate(new SKSize(100, 100)).ShouldBeTrue();

        root.Available.ShouldBe(UsLetter);
    }

    [Fact]
    public void Allocated_UnChangedUntilCommitted()
    {
        var root = new RootLayoutContext(UsLetter);
        root.Available.ShouldBe(UsLetter);
        root.Allocated.Size.Height.ShouldBe(0);

        var child = root.GetVerticalChildContext(UsLetter);
        child.TryAllocate(new SKSize(100, 100)).ShouldBeTrue();

        root.Allocated.Size.Height.ShouldBe(0);
    }
}
