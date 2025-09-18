namespace InvoiceKit.Pdf.Containers;

using SkiaSharp;

/// <summary>
/// Used to render a layout across a single page.
/// </summary>
public class PageLayout : IPage, IDisposable
{
    /// <summary>
    /// The unmodified original available drawing area of the page.
    /// </summary>
    private SKRect Drawable { get; }

    /// <summary>
    /// The current available drawing area as a SKRect.
    /// </summary>
    public SKRect Available => new (_cursor.X, _cursor.Y, Drawable.Width, Drawable.Height);

    /// <summary>
    /// Enumerable of drawables that fit onto the page.
    /// </summary>
    public List<IDrawable> Drawables { get; } = [];

    /// <summary>
    /// The current available drawing area as a SKSize.
    /// </summary>
    public SKSize AvailableSize => Available.Size;

    /// <summary>
    /// Boolean used to check if the page has been fully drawn.
    /// </summary>
    public bool IsFullyDrawn { get; private set; }

    /// <summary>
    /// The next available drawing position on the page.
    /// </summary>
    private SKPoint _cursor;

    public PageLayout(SKRect drawable, bool debug)
    {
        Drawable = drawable;
        _cursor = Drawable.Location;
    }

    public void MarkFullyDrawn(PageLayout page)
    {
        IsFullyDrawn = true;
    }

    public void AddDrawable(IDrawable drawable)
    {
        Drawables.Add(drawable);
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

        ForceAllocateSize(rect.Size);
        return true;
    }

    /// <summary>
    /// Allocates a rect, even if it overflows the available area.
    /// </summary>
    private void ForceAllocateSize(SKSize size)
    {
        _cursor.Offset(0, size.Height);
    }

    public void Dispose()
    {
    }
}
