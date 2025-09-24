namespace InvoiceKit.Pdf;

using SkiaSharp;

public class LayoutContext : ILayoutContext
{
    public SKRect Available { get; }

    public SKPoint Cursor => Available.Location;

    internal LayoutContext(SKRect available)
    {
        Available = available;
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

        return true;
    }

    /// <summary>
    /// Allocates a rect, even if it overflows the available area.
    /// </summary>
    public void ForceAllocateSize(SKSize size)
    {
        Cursor.Offset(0, size.Height);
    }
}
