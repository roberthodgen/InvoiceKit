namespace InvoiceKit.Pdf;

using SkiaSharp;

public abstract class LayoutContextBase : ILayoutContext
{
    protected readonly List<float> _allocated = [];

    protected readonly SKRect _originalSpace;

    public abstract SKRect Allocated { get; }

    public abstract SKRect Available { get; }

    protected LayoutContextBase(SKRect available)
    {
        _originalSpace = available;
    }

    public bool TryAllocate(SKSize size)
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

    public void CommitChildContext(ILayoutContext child)
    {
        if (ReferenceEquals(child, this))
        {
            return;
        }

        _allocated.Add(child.Allocated.Height);
    }

    public ILayoutContext GetVerticalChildContext(SKRect intersectingRect)
    {
        return new VerticalLayoutContext(SKRect.Intersect(Available, intersectingRect));
    }

    public ILayoutContext GetHorizontalChildContext(SKRect intersectingRect)
    {
        return new HorizonalLayoutContext(SKRect.Intersect(Available, intersectingRect));
    }
}
