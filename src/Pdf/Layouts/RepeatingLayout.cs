namespace InvoiceKit.Pdf.Layouts;

using Geometry;

internal class RepeatingLayout(ILayout child) : ILayout
{
    public LayoutResult Layout(ILayoutContext context)
    {
        return LayoutResult.Deferred([ChildLayout.CreateChild(child, context),]);
    }

    public ILayoutContext GetContext(ILayoutContext parentContext)
    {
        return parentContext.GetRepeatingChildContext();
    }

    public ILayoutContext GetContext(ILayoutContext parentContext, OuterRect intersectingRect)
    {
        return parentContext.GetRepeatingChildContext(intersectingRect);
    }
}
