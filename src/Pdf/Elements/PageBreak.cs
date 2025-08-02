namespace InvoiceKit.Pdf.Elements;

using Layouts;
using SkiaSharp;

public sealed class PageBreak : IDrawable
{
    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, available.Height + 1); // just larger
    }

    public void Draw(PageLayout page, SKRect rect)
    {
        // nothing to draw
    }

    public void Dispose()
    {
    }
}
