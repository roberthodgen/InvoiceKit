namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

internal class PageBreakLayout : ILayout
{
    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, available.Height);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        // Takes up the rest of available space, putting the footer at the bottom of the page.
        context.TryAllocate(this);
        return LayoutResult.FullyDrawn([]);
    }
}
