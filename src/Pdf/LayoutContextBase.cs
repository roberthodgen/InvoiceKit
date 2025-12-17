namespace InvoiceKit.Pdf;

using SkiaSharp;

public abstract class LayoutContextBase : ILayoutContext
{
    private bool _committed;

    protected readonly LayoutContextBase? Parent;

    protected readonly List<float> AllocatedHeights = [];

    protected readonly SKRect OriginalSpace;

    public abstract SKRect Allocated { get; }

    public virtual SKRect Available => SKRect.Intersect(
        Parent!.Available,
        new (
            OriginalSpace.Left,
            Allocated.Bottom,
            OriginalSpace.Right,
            OriginalSpace.Bottom));

    protected LayoutContextBase(SKRect available, LayoutContextBase? parent)
    {
        OriginalSpace = available;
        Parent = parent;
    }

    public virtual bool TryAllocate(SKSize size)
    {
        if (size.Height > Available.Height || size.Width > Available.Width)
        {
            return false;
        }

        AllocatedHeights.Add(size.Height);
        return true;
    }

    public bool TryAllocate(SKSize size, out SKRect rect)
    {
        rect = new SKRect(Available.Left, Available.Top, Available.Left + size.Width, Available.Top + size.Height);
        return TryAllocate(size);
    }

    public virtual void CommitChildContext()
    {
        if (_committed)
        {
            throw new ApplicationException("Cannot commit child context twice.");
        }

        _committed = true;
        Parent?.AllocatedHeights.Add(Allocated.Height);
    }

    public ILayoutContext GetVerticalChildContext(SKRect intersectingRect)
    {
        return new VerticalLayoutContext(SKRect.Intersect(Available, intersectingRect), this);
    }

    public ILayoutContext GetHorizontalChildContext(SKRect intersectingRect)
    {
        return new HorizonalLayoutContext(SKRect.Intersect(Available, intersectingRect), this);
    }
}
