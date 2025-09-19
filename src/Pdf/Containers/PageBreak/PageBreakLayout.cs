namespace InvoiceKit.Pdf.Containers.PageBreak;

using SkiaSharp;

public class PageBreakLayout : ILayout
{
    public SKSize Measure(SKSize available)
    {
        throw new NotImplementedException();
    }

    public void LayoutPages(MultiPageContext context, bool debug)
    {
        // Marks the page fully drawn. When another layout calls getCurrentPage(), it will get a new page.
        context.GetCurrentPage().MarkFullyDrawn();
    }
}
