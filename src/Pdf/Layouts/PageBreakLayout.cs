namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

internal class PageBreakLayout : ILayout
{
    private bool _drawn;

    public SKSize Measure(SKRect available)
    {
        return new SKSize(available.Width, available.Height);
    }

    public LayoutResult Layout(LayoutContext context, LayoutType layoutType)
    {
        // Repeating elements should not have page breaks.
        if (_drawn || layoutType == LayoutType.RepeatingElement)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        _drawn = true;
        return new LayoutResult([], LayoutStatus.NeedsNewPage);
    }
}
