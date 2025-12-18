namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using InvoiceKit.Pdf.Geometry;

public class VerticalLayoutContextTests
{
    /// <summary>
    /// Points per inch.
    /// </summary>
    private const float Ppi = 72f;

    private static readonly OuterRect UsLetter = new (0, 0, 8.5f * Ppi, 11f * Ppi);

    private readonly RootLayoutContext _root = new (UsLetter.ToRect());

    [Fact]
    public void TryAllocate_DecreasesAvailable()
    {
        var context = _root.GetVerticalChildContext(UsLetter);
        context.TryAllocate(new OuterSize(100, 100)).ShouldBeTrue();

        context.Available.Height.ShouldBe(UsLetter.Height - 100);
    }

    [Fact]
    public void TryAllocate_IncreasesAllocated()
    {
        var context = _root.GetVerticalChildContext(UsLetter);
        context.TryAllocate(new OuterSize(100, 100)).ShouldBeTrue();

        context.Allocated.Size.Height.ShouldBe(100);
    }

    [Fact]
    public void Available_DecreasesAsChildrenAreCommitted()
    {
        var parent = _root.GetVerticalChildContext(UsLetter);
        var child = parent.GetVerticalChildContext(UsLetter);
        child.TryAllocate(new OuterSize(100, 100)).ShouldBeTrue();

        // Act
        child.CommitChildContext();

        // Assert
        parent.Available.ToSize().Height.ShouldBe(UsLetter.Height - 100);
    }

    [Fact]
    public void Allocated_IncreasesAsChildrenAreCommitted()
    {
        var parent = _root.GetVerticalChildContext(UsLetter);
        var child = parent.GetVerticalChildContext(UsLetter);
        child.TryAllocate(new OuterSize(100, 100)).ShouldBeTrue();

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
        child.TryAllocate(new OuterSize(100, 100)).ShouldBeTrue();

        parent.Available.ShouldBe(UsLetter);
    }

    [Fact]
    public void Allocated_UnChangedUntilCommitted()
    {
        var parent = _root.GetVerticalChildContext(UsLetter);
        var child = parent.GetVerticalChildContext(UsLetter);
        child.TryAllocate(new OuterSize(100, 100)).ShouldBeTrue();

        parent.Allocated.Size.Height.ShouldBe(0);
    }
}
