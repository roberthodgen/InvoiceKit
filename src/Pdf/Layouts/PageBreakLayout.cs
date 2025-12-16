namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

internal class PageBreakLayout : ILayout
{
    private bool IsDrawn;

    public LayoutResult Layout(LayoutContext context)
    {
        if (IsDrawn)
        {
            return LayoutResult.FullyDrawn([]);
        }
        IsDrawn = true;
        return LayoutResult.NeedsNewPage([]);
    }
}
