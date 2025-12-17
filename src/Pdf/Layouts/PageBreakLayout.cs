namespace InvoiceKit.Pdf.Layouts;

internal class PageBreakLayout : ILayout
{
    private bool _isDrawn;

    public LayoutResult Layout(ILayoutContext context)
    {
        if (_isDrawn)
        {
            return LayoutResult.FullyDrawn([]);
        }

        _isDrawn = true;
        return LayoutResult.NeedsNewPage([]);
    }

    public ILayoutContext GetContext(ILayoutContext parentContext)
    {
        return parentContext.GetVerticalChildContext(parentContext.Available);
    }
}
