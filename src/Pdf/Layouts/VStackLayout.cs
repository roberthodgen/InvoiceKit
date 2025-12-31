namespace InvoiceKit.Pdf.Layouts;

using Geometry;

internal class VStackLayout(List<ILayout> children) : ILayout
{
    public LayoutResult Layout(ILayoutContext context)
    {
        if (children.Count == 0)
        {
            return LayoutResult.FullyDrawn([]);
        }

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
