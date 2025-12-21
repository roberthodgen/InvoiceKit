namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using Geometry;

internal class HorizontalRuleLayout(BlockStyle style) : ILayout
{
    public LayoutResult Layout(ILayoutContext context)
    {
        var contentWidth = context.Available.ToContentRect(style).ToSize().Width;
        var contentSize = new ContentSize(contentWidth, 0); // 0 height
        if (context.TryAllocate(contentSize.ToOuterSize(style), out var rect))
        {
            return LayoutResult.FullyDrawn([new BorderDrawable(rect, style),]);
        }

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
