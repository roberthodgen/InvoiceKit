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
    public SKRect Drawable { get; }

    /// <summary>
    /// The current canvas available for drawing.
    /// </summary>
    public SKCanvas Canvas { get; }

    public SKRect Available => new (_cursor.X, _cursor.Y, Drawable.Width, Drawable.Height);

    public bool Debug { get; }

    /// <summary>
    /// The next available drawing position on the page.
    /// </summary>
    private SKPoint _cursor;

    public PageLayout(SKCanvas canvas, SKSize size, SKRect drawable, bool debug)
    {
        Canvas = canvas;
        Size = size;
        Drawable = drawable;
        Debug = debug;
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

    public void DrawBlock(IDrawable block)
    {
        var measured = block.Measure(Available.Size);
        if (measured.Height > Available.Height || measured.Width > Available.Width)
        {
            throw new NotImplementedException("TODO page break logic"); // delegate to PdfDocumentBuilder to end page, begin new one, reset cursor
        }

        block.Draw(this, Available);
    }

    public bool TryAllocateRect(SKSize size, out SKRect rect)
    {
        if (size.Height > Available.Height || size.Width > Available.Width)
        {
            rect = SKRect.Empty;
            return false;
        }

        ForceAllocateRect(size, out rect);
        return true;
    }

    public void ForceAllocateRect(SKSize size, out SKRect rect)
    {
        rect = SKRect.Create(Available.Location, size);
        _cursor.Offset(0, size.Height);
    }

    public void Dispose()
    {
        Canvas.Dispose();
    }
}
