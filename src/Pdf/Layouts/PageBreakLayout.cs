namespace InvoiceKit.Pdf.Layouts;

using Geometry;

internal class PageBreakLayout : ILayout
{
    private bool _isDrawn;

    public LayoutResult Layout(ILayoutContext context)
    {
        if (context.Repeating)
        {
            return LayoutResult.FullyDrawn([]); // can't break a repeating layout
        }

        if (_isDrawn)
        {
            return LayoutResult.FullyDrawn([]);
        }

        _isDrawn = true;
        return LayoutResult.NeedsNewPage([]);
    }

    public ILayoutContext GetContext(ILayoutContext parentContext)
    {
        return parentContext.GetVerticalChildContext();
    }

    public ILayoutContext GetContext(ILayoutContext parentContext, OuterRect intersectingRect)
    {
        return parentContext.GetVerticalChildContext(intersectingRect);
    }
}
