namespace InvoiceKit.Pdf.Elements.Images;

using Containers;
using SkiaSharp;

internal class BitmapImageDrawable : IDrawable
{
    private readonly SKBitmap _bitmap;

    public SKRect SizeAndLocation { get; }

    public bool Debug { get; }

    internal BitmapImageDrawable(SKBitmap bitmap, SKRect rect, bool debug = false)
    {
        _bitmap = bitmap;
        SizeAndLocation = rect;
        Debug = debug;
    }

    public void Draw(SKCanvas canvas, Page page)
    {
        canvas.DrawBitmap(_bitmap, SizeAndLocation);
    }

    public void Dispose()
    {
        _bitmap.Dispose();
    }
}
