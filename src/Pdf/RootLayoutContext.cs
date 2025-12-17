namespace InvoiceKit.Pdf;

using SkiaSharp;

public sealed class RootLayoutContext : LayoutContextBase
{
    public override SKRect Allocated => new (
        _originalSpace.Left,
        _originalSpace.Top,
        _originalSpace.Right,
        _originalSpace.Top + _allocated.Sum());

    public override SKRect Available => new (
        _originalSpace.Left,
        Allocated.Bottom,
        _originalSpace.Right,
        _originalSpace.Bottom);

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
