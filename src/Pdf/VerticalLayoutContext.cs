namespace InvoiceKit.Pdf;

using SkiaSharp;

public sealed class VerticalLayoutContext : LayoutContextBase
{
    public override SKRect Allocated => new (
        _originalSpace.Left,
        _originalSpace.Top,
        _originalSpace.Right,
        (Math.Max(_originalSpace.Top, _parent!.Available.Top)) + _allocated.Sum());

    internal VerticalLayoutContext(SKRect available, LayoutContextBase? parent)
        : base(available, parent)
    {
    }
}
