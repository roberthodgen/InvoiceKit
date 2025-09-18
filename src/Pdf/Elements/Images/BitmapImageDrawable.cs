namespace InvoiceKit.Pdf.Elements.Images;

using Containers;
using SkiaSharp;

internal class BitmapImageDrawable : IDrawable
{
    private readonly SKBitmap _bitmap;

    public SKRect SizeAndLocation { get; }

    internal BitmapImageDrawable(string path, SKRect rect)
    {
        using var data = SKData.Create(path);
        using var codec = SKCodec.Create(data);
        _bitmap = SKBitmap.Decode(codec);
        SizeAndLocation = rect;
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(_bitmap.Width, _bitmap.Height);
    }

    public void Draw(SKCanvas canvas, PageLayout page)
    {
        canvas.DrawBitmap(_bitmap, page.Available.Location);
    }

    public void Dispose()
    {
        _bitmap.Dispose();
    }
}
