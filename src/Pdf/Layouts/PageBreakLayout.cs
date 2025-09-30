namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

internal class PageBreakLayout : ILayout
{
    private bool _drawn;

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, available.Height);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        if (_drawn)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        _drawn = true;
        return new LayoutResult([], LayoutStatus.NeedsNewPage);
    }
}
