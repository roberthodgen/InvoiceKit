namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

internal class SpacingBlockLayout(float height) : ILayout
{
    public LayoutResult Layout(ILayoutContext context)
    {
        // Allocates the space but does not draw anything.
        context.TryAllocate(new SKSize(0, height));
        return LayoutResult.FullyDrawn([]);
    }

    public ILayoutContext GetContext(ILayoutContext parentContext)
    {
        return parentContext.GetVerticalChildContext();
    }

    public ILayoutContext GetContext(ILayoutContext parentContext, SKRect intersectingRect)
    {
        return parentContext.GetVerticalChildContext(intersectingRect);
    }
}
