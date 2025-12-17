namespace InvoiceKit.Pdf;

using SkiaSharp;

public sealed class VerticalLayoutContext : LayoutContextBase
{
    public override SKRect Allocated => new (
        OriginalSpace.Left,
        OriginalSpace.Top,
        OriginalSpace.Right,
        (Math.Max(OriginalSpace.Top, Parent!.Available.Top)) + AllocatedHeights.Sum());

    internal VerticalLayoutContext(SKRect available, LayoutContextBase? parent)
        : base(available, parent)
    {
    }
}
