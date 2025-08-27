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
    /// The current canvas available for drawing.
    /// </summary>
    public SKCanvas Canvas { get; }

    /// <summary>
    /// The current available drawing area as a SKRect.
    /// </summary>
    public SKRect Available => new (_cursor.X, _cursor.Y, Drawable.Width, Drawable.Height);

    /// <summary>
    /// Enumerable of drawables that fit onto the page.
    /// </summary>
    public IEnumerable<IDrawable> Drawables { get; } = [];

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

    public PageLayout(SKCanvas canvas, SKRect drawable, bool debug)
    {
        Canvas = canvas;
        Drawable = drawable;
        _cursor = Drawable.Location;

        if (debug)
        {
            canvas.DrawRect(
                Available,
                new SKPaint
                {
                    Style = SKPaintStyle.Stroke,
                    Color = SKColors.Magenta,
                    StrokeWidth = .5f,
                });
        }
    }

    public void SetFullyDrawn()
    {
        IsFullyDrawn = true;
    }

    /// <summary>
    /// Used to determine if a block can be drawn on the current page.
    /// </summary>
    public bool TryAllocateSize(SKSize size)
    {
        if (size.Height > Available.Height || size.Width > Available.Width)
        {
            return false;
        }

        ForceAllocateSize(size);
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
        Canvas.Dispose();
    }
}
