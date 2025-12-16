namespace InvoiceKit.Pdf;

using SkiaSharp;

public sealed class HorizonalLayoutContext : LayoutContextBase
{
    internal HorizonalLayoutContext(SKRect available)
        : base(available)
    {
        _allocated.Add(0); // prevent error on .Max
    }

    public override SKRect Allocated => new (
        _originalSpace.Left,
        _originalSpace.Top,
        _originalSpace.Right,
        _originalSpace.Top + _allocated.Max()); // Max

    public override SKRect Available => new (
        _originalSpace.Left,
        Allocated.Bottom,
        _originalSpace.Right,
        _originalSpace.Bottom);
}
