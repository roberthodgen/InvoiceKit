namespace InvoiceKit.Pdf;

using SkiaSharp;

public sealed class RootLayoutContext : LayoutContextBase
{
    public override SKRect Allocated => new (
        OriginalSpace.Left,
        OriginalSpace.Top,
        OriginalSpace.Right,
        OriginalSpace.Top + AllocatedHeights.Sum());

    public override SKRect Available => new (
        OriginalSpace.Left,
        Allocated.Bottom,
        OriginalSpace.Right,
        OriginalSpace.Bottom);

    public RootLayoutContext(SKRect available)
        : base(available, null)
    {
    }

    public override void CommitChildContext()
    {
        // Nothing to do
    }

    public override bool TryAllocate(SKSize size)
    {
        throw new ApplicationException("Cannot allocate in root context.");
    }
}
