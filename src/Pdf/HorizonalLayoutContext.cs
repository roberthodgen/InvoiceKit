namespace InvoiceKit.Pdf;

using SkiaSharp;

public sealed class HorizonalLayoutContext : LayoutContextBase
{
    public override SKRect Allocated => new (
        OriginalSpace.Left,
        OriginalSpace.Top,
        OriginalSpace.Right,
        OriginalSpace.Top); // does not account for allocated height

    internal HorizonalLayoutContext(SKRect available, LayoutContextBase? parent)
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
            Parent?.TryAllocate(new SKSize(OriginalSpace.Width, AllocatedHeights.Max()));
        }
    }
}
