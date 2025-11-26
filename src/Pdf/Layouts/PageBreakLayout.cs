namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

internal class PageBreakLayout : ILayout
{
    private bool _isDrawn;

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, available.Height);
    }

    public LayoutResult Layout(LayoutContext context)
    {
        if (_isDrawn)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        // Takes up the rest of the page space, putting the footer at the bottom of the page.
        context.TryAllocate(this);
        _isDrawn = true;

        // Calls for a new page with no drawables.
        return new LayoutResult([], LayoutStatus.NeedsNewPage);
    }
}
