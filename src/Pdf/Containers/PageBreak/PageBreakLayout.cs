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
        // Marks the page fully drawn. When another layout calls getCurrentPage(), it will get a new page.
        return new LayoutResult([], LayoutState.IsFullyDrawn);
    }
}
