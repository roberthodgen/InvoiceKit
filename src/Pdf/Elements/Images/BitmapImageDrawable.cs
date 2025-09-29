namespace InvoiceKit.Pdf.Elements.Images;

using SkiaSharp;

internal class BitmapImageDrawable : IDrawable
{
    private readonly SKBitmap _bitmap;

    public SKRect SizeAndLocation { get; }

    internal BitmapImageDrawable(SKBitmap bitmap, SKRect rect)
    {
        _bitmap = bitmap;
        SizeAndLocation = rect;
    }

    public void Draw(IDrawableContext context)
    {
        context.Canvas.DrawBitmap(_bitmap, SizeAndLocation);
    }

    public void Dispose()
    {
        _bitmap.Dispose();
    }
}
