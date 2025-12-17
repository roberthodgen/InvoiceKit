namespace InvoiceKit.Pdf;

using SkiaSharp;

public abstract class LayoutContextBase : ILayoutContext
{
    private bool _committed;

    protected readonly LayoutContextBase? _parent;

    protected readonly List<float> _allocated = [];

    protected readonly SKRect _originalSpace;

    public abstract SKRect Allocated { get; }

    public virtual SKRect Available => SKRect.Intersect(
        _parent!.Available,
        new (
            _originalSpace.Left,
            Allocated.Bottom,
            _originalSpace.Right,
            _originalSpace.Bottom));

    protected LayoutContextBase(SKRect available, LayoutContextBase? parent)
    {
        _originalSpace = available;
        _parent = parent;
    }

    public virtual bool TryAllocate(SKSize size)
    {
        if (size.Height > Available.Height || size.Width > Available.Width)
        {
            return false;
        }

        _allocated.Add(size.Height);
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
        _parent?._allocated.Add(Allocated.Height);
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
