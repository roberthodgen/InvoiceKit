namespace InvoiceKit.Pdf.Containers.PageBreak;

using SkiaSharp;

internal class PageBreakLayout : ILayout
{
    public bool IsFullyDrawn { get; set; }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, available.Height);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        if (IsFullyDrawn)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        IsFullyDrawn = true;
        // Requests a new page and returns no drawables.
        return new LayoutResult([], LayoutStatus.NeedsNewPage);
    }
}
