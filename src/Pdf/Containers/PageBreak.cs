namespace InvoiceKit.Pdf.Containers;

using SkiaSharp;

public sealed class PageBreak : IViewBuilder
{
    public SKSize Measure(SKSize available)
    {
        // Returns the size for the rest of the available space on the page.
        return new SKSize(available.Width, available.Height);
    }

    public void Draw(PageLayout page)
    {
        page.MarkFullyDrawn();
    }

    public void Dispose()
    {
    }

    public ILayout ToLayout(MultiPageContext context)
    {
        throw new NotImplementedException();
    }
}
