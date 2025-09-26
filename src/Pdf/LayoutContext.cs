namespace InvoiceKit.Pdf;

using SkiaSharp;

public class LayoutContext : ILayoutContext
{
    private readonly List<float> _allocated = [];

    public float Allocated => _allocated.Sum();

    public SKRect Available
    {
        get
        {
            var rect = new SKRect(
                _originalSpace.Left,
                _originalSpace.Top + Allocated,
                _originalSpace.Right,
                _originalSpace.Bottom);
            return rect;
        }
    }

    private readonly SKRect _originalSpace;

    internal LayoutContext(SKRect available)
    {
        _originalSpace = available;
    }

    public void CommitChildContext(LayoutContext child)
    {
        _allocated.Add(child.Allocated);
    }

    /// <summary>
    /// Used to determine if a block can be drawn on the current page.
    /// </summary>
    public bool TryAllocateRect(SKRect rect)
    {
        if (rect.Height > Available.Height || rect.Width > Available.Width)
        {
            return false;
        }

        _allocated.Add(rect.Height);
        return true;
    }
}
