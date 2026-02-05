namespace InvoiceKit.Pdf;

using Geometry;
using SkiaSharp;

public abstract class LayoutContextBase : ILayoutContext
{
    protected bool _committed;

    protected readonly LayoutContextBase? Parent;

    protected readonly List<float> AllocatedHeights = [];

    protected readonly OuterRect OriginalSpace;

    public abstract SKRect Allocated { get; }

    public virtual OuterRect Available => OuterRect.Intersect(
        Parent!.Available,
        new OuterRect(
            OriginalSpace.Left,
            Allocated.Bottom,
            OriginalSpace.Right,
            OriginalSpace.Bottom));

    public bool Repeating { get; }

    protected LayoutContextBase(OuterRect available, LayoutContextBase? parent)
    {
        OriginalSpace = available;
        Parent = parent;
        Repeating = parent?.Repeating ?? false;
    }

    protected LayoutContextBase(OuterRect available, LayoutContextBase parent, bool repeating)
    {
        OriginalSpace = available;
        Parent = parent;
        Repeating = parent.Repeating || repeating;
    }

    public bool CanFit(OuterSize size)
    {
        if (size.Height > Available.Height || size.Width > Available.Width)
        {
            return false;
        }
        return true;
    }

    public bool TryAllocate(OuterSize outer)
    {
        if (CanFit(outer))
        {
            AllocatedHeights.Add(outer.Height);
            return true;
        }

        return false;
    }

    public bool TryAllocate(OuterSize outer, out OuterRect rect)
    {
        var size = outer.ToSize();
        rect = new OuterRect(Available.Left, Available.Top, Available.Left + size.Width, Available.Top + size.Height);
        return TryAllocate(outer);
    }

    public abstract void CommitChildContext();

    public ILayoutContext GetVerticalChildContext(OuterRect intersectingRect)
    {
        return new VerticalLayoutContext(OuterRect.Intersect(Available, intersectingRect), this);
    }

    public ILayoutContext GetVerticalChildContext()
    {
        return new VerticalLayoutContext(Available, this);
    }

    public HorizonalLayoutContext GetHorizontalChildContext(OuterRect intersectingRect)
    {
        return new HorizonalLayoutContext(OuterRect.Intersect(Available, intersectingRect), this);
    }

    public HorizonalLayoutContext GetHorizontalChildContext()
    {
        return new HorizonalLayoutContext(Available, this);
    }

    public ILayoutContext GetRepeatingChildContext()
    {
        return new RepeatingLayoutContext(Available, this);
    }

    public ILayoutContext GetRepeatingChildContext(OuterRect intersectingRect)
    {
        return new RepeatingLayoutContext(OuterRect.Intersect(Available, intersectingRect), this);
    }
}
