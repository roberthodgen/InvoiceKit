namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using Geometry;

internal class HorizontalRuleLayout(BlockStyle style) : ILayout
{
    public LayoutResult Layout(ILayoutContext context)
    {
        var width = context.Available.ToContentRect(style).ToSize().Width;
        var contentSize = new ContentSize(width, 0);
        var paddingSize = style.Padding.ToSize(contentSize);
        var borderSize = style.Border.ToSize(paddingSize);
        var outerSize = style.Margin.ToSize(borderSize);
        if (context.TryAllocate(outerSize, out var rect))
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
