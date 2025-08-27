namespace InvoiceKit.Pdf.Elements.Images;

using Containers;
using SkiaSharp;

internal class BitmapImageDrawable : IDrawable
{
    private readonly SKBitmap _bitmap;

    internal BitmapImageDrawable(string path)
    {
        using var data = SKData.Create(path);
        using var codec = SKCodec.Create(data);
        _bitmap = SKBitmap.Decode(codec);
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(_bitmap.Width, _bitmap.Height);
    }

    public void Draw(PageLayout page)
    {
        page.Canvas.DrawBitmap(_bitmap, page.Available.Location);
    }

    public void Dispose()
    {
        _bitmap.Dispose();
    }
}
