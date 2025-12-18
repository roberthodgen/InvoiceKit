namespace InvoiceKit.Pdf;

using Geometry;
using SkiaSharp;

/// <summary>
/// A root layout context is one that is root to each page being laid out. A new root layout context is created for each
/// page in a PDF document with the original page size.
/// </summary>
public sealed class RootLayoutContext : LayoutContextBase
{
    public override SKRect Allocated => new (
        OriginalSpace.Left,
        OriginalSpace.Top,
        OriginalSpace.Right,
        OriginalSpace.Top + AllocatedHeights.Sum());

    public override OuterRect Available => new (
        OriginalSpace.Left,
        Allocated.Bottom,
        OriginalSpace.Right,
        OriginalSpace.Bottom);

    public RootLayoutContext(SKRect available)
        : base(new (available), null)
    {
    }

    public override void CommitChildContext()
    {
        if (_committed)
        {
            throw new ApplicationException("Root context commited twice.");
        }

        _committed = true;
    }
}
