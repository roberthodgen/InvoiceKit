namespace InvoiceKit.Pdf.Containers.PageBreak;

using SkiaSharp;

public class PageBreakLayout : ILayout
{
    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, available.Height);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        // Requests new page and returns no drawables.
        return new LayoutResult([], LayoutStatus.NeedsNewPage);
    }
}
