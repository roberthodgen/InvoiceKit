namespace InvoiceKit.Pdf.Elements;

using Layouts;
using SkiaSharp;

public sealed class PageBreak : IDrawable
{
    public SKSize Measure(SKSize available)
    {
        // Returns the size for the rest of the available space on the page.
        return new SKSize(available.Width, available.Height);
    }

    public void Draw(PageLayout page)
    {
        page.SetFullyDrawn();
    }

    public void Dispose()
    {
    }
}
