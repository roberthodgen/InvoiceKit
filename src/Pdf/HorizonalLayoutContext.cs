namespace InvoiceKit.Pdf;

using SkiaSharp;

public sealed class HorizonalLayoutContext : LayoutContextBase
{
    public override SKRect Allocated => new (
        OriginalSpace.Left,
        OriginalSpace.Top,
        OriginalSpace.Right,
        OriginalSpace.Top + AllocatedHeights.Max());

    internal HorizonalLayoutContext(SKRect available, LayoutContextBase? parent)
        : base(available, parent)
    {
        AllocatedHeights.Add(0); // prevent error on .Max
    }
}
