namespace InvoiceKit.Pdf;

using SkiaSharp;

public sealed class HorizonalLayoutContext : LayoutContextBase
{
    public override SKRect Allocated => new (
        _originalSpace.Left,
        _originalSpace.Top,
        _originalSpace.Right,
        _originalSpace.Top + _allocated.Max());

    internal HorizonalLayoutContext(SKRect available, LayoutContextBase? parent)
        : base(available, parent)
    {
        _allocated.Add(0); // prevent error on .Max
    }
}
