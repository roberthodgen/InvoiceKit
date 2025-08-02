namespace InvoiceKit.Pdf.Elements.Images;

using Layouts;
using SkiaSharp;

internal class BitmapImage : IDrawable
{
    private readonly SKBitmap _bitmap;

    internal BitmapImage(string path)
    {
        using var data = SKData.Create(path);
        using var codec = SKCodec.Create(data);
        _bitmap = SKBitmap.Decode(codec);
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(_bitmap.Width, _bitmap.Height);
    }

    public void Draw(PageLayout page, SKRect rect, Func<PageLayout> getNextPage)
    {
        page.Canvas.DrawBitmap(_bitmap, rect.Location);
    }

    public void Dispose()
    {
        _bitmap.Dispose();
    }
}
