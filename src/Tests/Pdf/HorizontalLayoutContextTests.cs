namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using InvoiceKit.Pdf.Geometry;

public class HorizontalLayoutContextTests
{
    /// <summary>
    /// Points per inch.
    /// </summary>
    private const float Ppi = 72f;

    private static readonly OuterRect UsLetter = new (0, 0, 8.5f * Ppi, 11f * Ppi);

    private readonly RootLayoutContext _root = new (UsLetter.ToRect());

    [Fact]
    public void TryAllocate_KeepsOriginalAvailable()
    {
        var context = _root.GetHorizontalChildContext();
        context.TryAllocate(new OuterSize(100, 100)).ShouldBeTrue();

        context.Available.Height.ShouldBe(UsLetter.Height);
    }

    [Fact]
    public void TryAllocate_KeepsOriginalAllocated()
    {
        var context = _root.GetHorizontalChildContext();
        context.TryAllocate(new OuterSize(100, 100)).ShouldBeTrue();

        context.Allocated.Size.Height.ShouldBe(0);
    }

    [Fact]
    public void Available_UnchangedAsAsChildrenAreCommitted()
    {
        var parent = _root.GetHorizontalChildContext();
        var child = parent.GetVerticalChildContext();
        child.TryAllocate(new OuterSize(100, 100)).ShouldBeTrue();

        // Act
        child.CommitChildContext();

        // Assert
        parent.Available.ToSize().Height.ShouldBe(UsLetter.Height);
    }

    [Fact]
    public void Allocated_UnchangedAsChildrenAreCommitted()
    {
        var parent = _root.GetHorizontalChildContext();
        var child = parent.GetVerticalChildContext();
        child.TryAllocate(new OuterSize(100, 100)).ShouldBeTrue();

        // Act
        child.CommitChildContext();

        // Assert
        parent.Allocated.Size.Height.ShouldBe(0);
    }

    [Fact]
    public void Available_UnChangedUntilCommitted()
    {
        var parent = _root.GetHorizontalChildContext();
        var child = parent.GetVerticalChildContext();
        child.TryAllocate(new OuterSize(100, 100)).ShouldBeTrue();

        parent.Available.ShouldBe(UsLetter);
    }

    [Fact]
    public void Allocated_UnChangedUntilCommitted()
    {
        var parent = _root.GetHorizontalChildContext();
        var child = parent.GetVerticalChildContext();
        child.TryAllocate(new OuterSize(100, 100)).ShouldBeTrue();

        parent.Allocated.Size.Height.ShouldBe(0);
    }

    [Fact]
    public void CommitChildContext_DecreasesParentAvailable()
    {
        var parent = _root.GetVerticalChildContext();
        var child = parent.GetHorizontalChildContext();

        // Act
        child.TryAllocate(new OuterSize(100, 100)).ShouldBeTrue();
        child.CommitChildContext();

        // Assert
        parent.Available.ToSize().Height.ShouldBe(UsLetter.Height - 100);
        parent.Allocated.Size.Height.ShouldBe(100);
    }
}
