namespace InvoiceKit.Pdf.Layouts;

using Geometry;

internal class SpacingBlockLayout(float height) : ILayout
{
    public LayoutResult Layout(ILayoutContext context)
    {
        context.TryAllocate(new OuterSize(context.Available.Width, height));
        return LayoutResult.FullyDrawn([]);
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
