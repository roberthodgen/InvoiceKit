namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

/// <summary>
/// Used to render a layout across a single page.
/// </summary>
public class PageLayout : IDisposable
{
    /// <summary>
    /// The unmodified original page size, including margins.
    /// </summary>
    public SKSize Size { get; }

    /// <summary>
    /// The unmodified original available drawing area of the page (a subset of <see cref="Size"/> due to margins).
    /// </summary>
    private SKRect Drawable { get; }

    /// <summary>
    /// The current canvas available for drawing.
    /// </summary>
    public SKCanvas Canvas { get; }

    public SKRect Available => new (_cursor.X, _cursor.Y, Drawable.Width, Drawable.Height);

    public bool IsFullyDrawn { get; private set; }

    /// <summary>
    /// The next available drawing position on the page.
    /// </summary>
    private SKPoint _cursor;

    public PageLayout(SKCanvas canvas, SKSize size, SKRect drawable, bool debug)
    {
        Canvas = canvas;
        Size = size;
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

    /// <summary>
    /// Used to determine if a block can be drawn on the current page.
    /// </summary>
    public bool TryAllocateChild(IDrawable drawable)
    {
        var size = drawable.Measure(Available.Size);
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
