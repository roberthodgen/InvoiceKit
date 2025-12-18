namespace InvoiceKit.Pdf;

using Geometry;
using SkiaSharp;

public sealed class VerticalLayoutContext : LayoutContextBase
{
    public override SKRect Allocated => new (
        OriginalSpace.Left,
        OriginalSpace.Top,
        OriginalSpace.Right,
        (Math.Max(OriginalSpace.Top, Parent!.Available.Top)) + AllocatedHeights.Sum());

    internal VerticalLayoutContext(OuterRect available, LayoutContextBase? parent)
        : base(available, parent)
    {
    }

    public override void CommitChildContext()
    {
        if (_committed)
        {
            throw new ApplicationException("Cannot commit child context twice.");
        }

        _committed = true;
        if (AllocatedHeights.Count > 0)
        {
            Parent?.TryAllocate(new OuterSize(OriginalSpace.Width, AllocatedHeights.Sum()));
        }
    }
}
