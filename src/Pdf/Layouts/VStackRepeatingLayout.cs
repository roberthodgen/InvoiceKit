namespace InvoiceKit.Pdf.Layouts;

using Geometry;

internal class VStackRepeatingLayout(List<ILayout> children) : ILayout
{
    public LayoutResult Layout(ILayoutContext context)
    {
        return LayoutResult.Deferred(children.Select(child => ChildLayout.CreateChild(child, context)).ToList());
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
